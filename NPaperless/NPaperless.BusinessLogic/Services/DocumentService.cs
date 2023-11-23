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
using NPaperless.REST.DTOs;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;
using Minio;

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

        public ObjectResult CreateDocument(Document request, List<System.IO.Stream> documentStreams) 
        {
            MemoryStream concatenatedStream = new MemoryStream();

            foreach (Stream documentStream in documentStreams)
            {
                documentStream.Position = 0;
                documentStream.CopyTo(concatenatedStream);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket("npaperless-bucket")
                .WithObject(request.Title)
                .WithStreamData(concatenatedStream)
                .WithContentType("application/pdf");

            _minio.PutObjectAsync(putObjectArgs).Wait();


            DocumentBL documentBL = _mapper.Map<DocumentBL>(request);

            DocumentDAL documentDAL = _mapper.Map<DocumentDAL>(documentBL);

            var value = _repository.CreateDocument(documentDAL);

            var response = new ObjectResult(value);

            response.StatusCode = 200;

            return response;
        }

        public ObjectResult DeleteDocumentById(long Id)
        {
            throw new NotImplementedException();
        }
        public Document GetDocumentById(long Id)
        {
            throw new NotImplementedException();
        }
        public Document UpdateDocument(UpdateDocumentRequest request)
        {
            DocumentBL documentBL = _mapper.Map<DocumentBL>(request);

            DocumentDAL documentDAL = _mapper.Map<DocumentDAL>(documentBL);
            //var response = _repository

            //var result = new ObjectResult(response);

            throw new NotImplementedException();
        }

    }
}
