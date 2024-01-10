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

namespace NPaperless.BusinessLogic.Services
{
    public class DocumentService : IDocumentService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DocumentService));
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly IDocumentDALRepository _repository;
        private readonly IMinioClient _minio;

        public DocumentService(IMapper mapper, IValidator<DocumentBL> validator, IDocumentDALRepository repository, IMinioClient minio)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _minio = minio;
        }

        public ObjectResult CreateDocument(DocumentBL document) 
        {
            _logger.Info(document);

            SaveFileToMinIO(document.UploadDocument).Wait();

            DocumentDAL documentDAL = _mapper.Map<DocumentDAL>(document);

            var value = _repository.CreateDocument(documentDAL);

            var response = new ObjectResult(value);

            response.StatusCode = 200;

            return response;
        }

        public DocumentBL GetDocumentById(long Id)
        {
            throw new NotImplementedException();
        }

        protected async Task SaveFileToMinIO(IFormFile file)
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
                            .WithObject(file.Name)
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
