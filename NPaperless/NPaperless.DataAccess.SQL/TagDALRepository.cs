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
        private NPaperlessDbContext db;
        private bool disposed = false;
        public TagDALRepository()
        {
            db = new NPaperlessDbContext();
        }
        public void AddTag(TagDAL document)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(int tagID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagDAL> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public TagDAL GetTag(int tagID)
        {
            throw new NotImplementedException();
        }

        public void UpdateTag(TagDAL tag)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.Database.EnsureCreated();
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
