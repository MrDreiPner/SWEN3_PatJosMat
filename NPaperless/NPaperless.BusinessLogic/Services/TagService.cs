using AutoMapper;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.Extensions.Options;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.Interfaces;
using NPaperless.REST.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Services
{
    internal class TagService
    {
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly ITagDALRepository _repository;

        public TagService(IMapper mapper, IValidator<TagBL> validator)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public int CreateTag(CreateTagRequest request)
        {
            TagBL tagBL = _mapper.Map<TagBL>(request);

            TagDAL tagDAL = _mapper.Map<TagDAL>(tagBL);

            _repository.AddTag(tagDAL);

            return 200;
        }
    }
}
