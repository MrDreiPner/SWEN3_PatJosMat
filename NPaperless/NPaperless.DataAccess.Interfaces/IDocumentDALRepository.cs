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
        public int CreateDocument(DocumentDAL document);
        public void UpdateDocument(int documentID, string text);
        public void DeleteDocument(int documentID);
        public void Save();
    }
}
