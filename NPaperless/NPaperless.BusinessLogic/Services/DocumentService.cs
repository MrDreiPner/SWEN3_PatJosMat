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

namespace NPaperless.BusinessLogic.Services
{
    public class DocumentService : IDocumentService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DocumentService));
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly IDocumentDALRepository _repository;
        private readonly IMinioClient _minio;
        /*private readonly IMessageSender _messageSender;*/

        public DocumentService(IMapper mapper, IValidator<DocumentBL> validator, IDocumentDALRepository repository, IMinioClient minio/*, IMessageSender messageSender*/)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _minio = minio;
            /*_messageSender = messageSender;*/
        }

        public HttpStatusCode CreateDocument(DocumentBL document) 
        {
            _logger.Info("creating document");

            //TODO validate file

            //TODO fix database
            DocumentDAL documentDAL = _mapper.Map<DocumentDAL>(document);
            int fileId = _repository.CreateDocument(documentDAL);

            _logger.Info("new File Id" + fileId);

            SaveFileToMinIO(document.UploadDocument).Wait();

            //_messageSender.SendMessage(fileId.ToString());

            //TODO exception handling

            var response = HttpStatusCode.OK;

            return response;
        }

        public DocumentBL GetDocumentById(long Id)
        {
            throw new NotImplementedException();
        }

        protected async Task SaveFileToMinIO(IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

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
                Console.WriteLine($"Minio Error: {e.Message}");
            }

        }
    }
}
