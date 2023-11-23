using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface ITagService {
        public List<Tag> GetTags(int? page, bool? fullPerms);
        public BusinessLogicResult<Tag> NewTag(Tag tag);
        public BusinessLogicResult<Tag> UpdateTag(int id, Tag tag);
        public BusinessLogicResult DeleteTag(int id);
    }

}