using NPaperless.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.DataAccess.Interfaces
{
    public interface IDocumentDALRepository : IDisposable
    {
        public DocumentDAL GetDocument(int documentID);
        public IEnumerable<DocumentDAL> GetAllDocuments();
        public void AddDocument(DocumentDAL document);
        public void UpdateDocument(DocumentDAL document);
        public void DeleteDocument(int documentID);
        public void Save();
    }
}
