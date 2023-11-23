using NPaperless.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.DataAccess.Interfaces 
{ 
    public interface ITagDALRepository : IDisposable
    {
        public TagDAL GetTag(int tag);
        public IEnumerable<TagDAL> GetAllTags();
        public TagDAL CreateTag(TagDAL tag);
        public void UpdateTag(TagDAL tag);
        public void DeleteTag(int tagID);
        public void Save();
    }
}
