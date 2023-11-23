using log4net;
using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.Interfaces;

namespace NPaperless.DataAccess.SQL
{
    public class DocumentDALRepository : IDocumentDALRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DocumentDALRepository));

        private NPaperlessDbContext _db;
        private bool disposed = false;
        public DocumentDALRepository()
        {
            _db = new NPaperlessDbContext();
        }
        public DocumentDAL CreateDocument(DocumentDAL document)
        {
            _logger.Info("Creating tag" + document);
            _db.Documents.Add(document);
            _logger.Info("Saving changes" + document);
            Save();
            Dispose();
            return document;
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