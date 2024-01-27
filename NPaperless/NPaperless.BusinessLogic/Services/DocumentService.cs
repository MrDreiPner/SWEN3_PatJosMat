using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPaperless.BusinessLogic.Interfaces;
using AutoMapper;
using FluentValidation;
using NPaperless.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;
using Minio;
using Microsoft.AspNetCore.Http;
using Minio.Exceptions;
using System.Net.Mime;
using System.Net;
using NPaperless.BusinessLogic.RabbitMQ;
using log4net.Repository.Hierarchy;
using System.IO.Enumeration;
using System.Reflection.Metadata;

namespace NPaperless.BusinessLogic.Services
{
    public class DocumentService : IDocumentService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DocumentService));
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly IDocumentDALRepository _repository;
        private readonly IMinioClient _minio;
        private readonly IMessageSender _messageSender;
        private readonly IOcrClient _crClient;

        public DocumentService(IMapper mapper, IValidator<DocumentBL> validator, IDocumentDALRepository repository, IMinioClient minio, IMessageSender messageSender)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _minio = minio;
            _messageSender = messageSender;
        }

        public async Task<IActionResult> CreateDocument(DocumentBL document) 
        {
            _logger.Info("creating document");
            try
            {
                var validationContext = new ValidationContext<DocumentBL>(document);
                var validationResult = _validator.Validate(validationContext);
                if (!validationResult.IsValid)
                {
                    return new BadRequestResult();
                }

                DocumentDAL documentDAL = _mapper.Map<DocumentDAL>(document);
                int fileId = _repository.CreateDocument(documentDAL);
                string uniqueFileName = generateUniqueFileName(document.UploadDocument.FileName);
                await SaveFileToMinIO(document.UploadDocument, uniqueFileName);
                _logger.Info("fileId:" + fileId.ToString() + ", fileName:" + uniqueFileName + " stored");
                _messageSender.SendMessage(fileId.ToString() + ", " + uniqueFileName);
                _logger.Info("fileId:" + fileId.ToString() + ", fileName:" + uniqueFileName + " queued");



                return new OkResult();
            }

            catch
            (Exception e)
            {
                _logger.Error($"Upload Document Error: {e.Message}");
                return new StatusCodeResult(500);
            }

        }

        public DocumentBL GetDocumentById(long Id)
        {
            throw new NotImplementedException();
        }

        protected string generateUniqueFileName(string passedFileName)
        {
            return passedFileName + "_" + Guid.NewGuid().ToString()  ;
        }

        protected async Task SaveFileToMinIO(IFormFile file, string uniqueFileName)
        {
            try
            {

                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    var memoryStream = new MemoryStream();
                    await streamReader.BaseStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var putObjectArgs = new PutObjectArgs()
                            .WithBucket("npaperless-bucket")
                            .WithObject(uniqueFileName)
                            .WithContentType(file.ContentType)
                            .WithStreamData(memoryStream)
                            .WithObjectSize(memoryStream.Length);


                    await _minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

                }
            }
            catch (MinioException e)
            {
                _logger.Error($"Minio Error: {e.Message}");
            }
        }
    }
}
