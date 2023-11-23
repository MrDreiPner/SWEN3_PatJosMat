using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.DataAccess.SQL
{
    public class TagDALRepository : ITagDALRepository
    {
        private readonly NPaperlessDbContext _db;
        private bool disposed = false;
        public TagDALRepository(NPaperlessDbContext db)
        {
            _db = db;
        }

        public TagDAL GetTag(int tagID)
        {
            return _db.Tags.Find(tagID);
        }

        public IEnumerable<TagDAL> GetAllTags()
        {
            return _db.Tags.ToList();
        }

        public TagDAL CreateTag(TagDAL tag)
        {
            _db.Tags.Add(tag);
            Save();
            return tag;
        }

        public void UpdateTag(TagDAL tag)
        {
            _db.Tags.Update(tag);
        }

        public void DeleteTag(int tagID)
        {
            TagDAL tag = _db.Tags.Find(tagID);
            _db.Tags.Remove(tag);
        }

        public void Save()
        {
            _db.Database.EnsureCreated();
            _db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
