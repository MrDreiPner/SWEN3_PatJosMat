using NPaperless.BusinessLogic.Entities;
using NPaperless.REST.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Mappers
{
    internal class DocumentMapper : Profile
    {
        public DocumentMapper()
        {
            CreateMap<Document, DocumentBL>().ReverseMap();
            CreateMap<UpdateDocumentRequest, DocumentBL>().ReverseMap();
        }
    }
}
