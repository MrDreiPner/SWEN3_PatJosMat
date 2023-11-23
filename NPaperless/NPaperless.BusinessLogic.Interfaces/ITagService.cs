using NPaperless.REST.DTOs;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface ITagService {
        public int CreateTag(CreateTagRequest tag);
    }

}