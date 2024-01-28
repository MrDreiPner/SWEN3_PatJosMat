using log4net;
using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.Interfaces;
using System.Reflection.Metadata;

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

        public DocumentDALRepository(NPaperlessDbContext db)
        {
            _db = db;
        }

        public int CreateDocument(DocumentDAL document)
        {
            _db.Documents.Add(document);
            Save();
            Dispose();
            _logger.Info("Added document with Id:" + document.OriginalFileName + " to Database");
            return document.Id;
        }

        public void DeleteDocument(int documentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentDAL> GetAllDocuments()
        {

        }

        public DocumentDAL GetDocument(int documentID)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(int documentID, string text)
        {
            _logger.Info("updating DB");
            var doc = _db.Documents.Find(documentID);
            _logger.Info("found doc");
            doc.Content = text;
            _db.Documents.Entry(doc).Property(x => x.Content).IsModified = true;
            _logger.Info("saving to DB");
            Save();
            Dispose();
            _logger.Info("Updated document with Id:" + doc.Id + ". Content: " + doc.Content);
            return;
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