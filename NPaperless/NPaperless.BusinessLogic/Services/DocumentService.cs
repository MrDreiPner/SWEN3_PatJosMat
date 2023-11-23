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

namespace NPaperless.BusinessLogic.Services
{
    public class DocumentService : IDocumentService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DocumentService));
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly IDocumentDALRepository _repository;

        public DocumentService(IMapper mapper, IValidator validator, IDocumentDALRepository repository)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
        }
        public ObjectResult CreateDocument(Document request) 
        { 
            DocumentBL documentBL = _mapper.Map<DocumentBL>(request);

            DocumentDAL documentDAL = _mapper.Map<DocumentDAL>(documentBL);

            //var response = _repository

            //var result = new ObjectResult(response);
            throw new NotImplementedException();
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
