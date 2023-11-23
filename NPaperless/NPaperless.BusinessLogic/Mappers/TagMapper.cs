using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NPaperless.REST.DTOs;
using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.Mappers
{
    internal class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<CreateTagRequest, TagBL>().ReverseMap();
        }
    }
}
