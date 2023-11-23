using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.Interfaces;

namespace NPaperless.DataAccess.SQL
{
    public class DocumentDALRepository : IDocumentDALRepository
    {
        private NPaperlessDbContext db;
        private bool disposed = false;
        public DocumentDALRepository()
        {
            db = new NPaperlessDbContext();
        }
        public void AddDocument(DocumentDAL document)
        {
            throw new NotImplementedException();
        }

        public void DeleteDocument(int documentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentDAL> GetAllDocuments()
        {
            throw new NotImplementedException();
        }

        public DocumentDAL GetDocument(int documentID)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(DocumentDAL document)
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