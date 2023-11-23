using NPaperless.REST.DTOs;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface ITagService {
        public ObjectResult CreateTag(CreateTagRequest tag);
        public ObjectResult UpdateTag(UpdateTagRequest tag);
    }

}