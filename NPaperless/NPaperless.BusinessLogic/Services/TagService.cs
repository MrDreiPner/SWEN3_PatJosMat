using AutoMapper;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.Extensions.Options;
using NPaperless.BusinessLogic.Entities;
using NPaperless.BusinessLogic.Interfaces;
using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.Interfaces;
using NPaperless.REST.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace NPaperless.BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TagService));
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly ITagDALRepository _repository;

        public TagService(IMapper mapper, IValidator<TagBL> validator, ITagDALRepository repository)
        {
            _logger.Info("Constructing " + this);
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
        }

        public ObjectResult CreateTag(CreateTagRequest request)
        {
            TagBL tagBL = _mapper.Map<TagBL>(request);

            TagDAL tagDAL = _mapper.Map<TagDAL>(tagBL);

            var response = _repository.CreateTag(tagDAL);

            return new ObjectResult(response);
        }

        public ObjectResult UpdateTag(UpdateTagRequest request)
        {
            TagBL tagBL = _mapper.Map<TagBL>(request);

            TagDAL tagDAL = _mapper.Map<TagDAL>(tagBL);

            var response = _repository.UpdateTag(tagDAL);
            
            return new ObjectResult(response);
        }
    }
}
