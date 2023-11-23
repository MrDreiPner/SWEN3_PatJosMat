using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;

namespace NPaperless.BusinessLogic.Mappers
{
    internal class DalMapper : Profile
    {
        public DalMapper()
        {
            CreateMap<CorrespondentBL, CorrespondentDAL>().ReverseMap();

            CreateMap<DocumentBL, DocumentDAL>().ReverseMap();

            CreateMap<DocumentTypeBL, DocumentTypeDAL>().ReverseMap();

            CreateMap<TagBL, TagDAL>().ReverseMap();
        }
    }
}
