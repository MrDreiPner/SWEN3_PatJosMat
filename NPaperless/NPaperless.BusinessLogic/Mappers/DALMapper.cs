﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;

namespace NPaperless.BusinessLogic.Mappers
{
    public class DalMapper : Profile
    {
        public DalMapper()
        {
            CreateMap<DocumentBL, DocumentDAL>().ReverseMap();
        }
    }
}
