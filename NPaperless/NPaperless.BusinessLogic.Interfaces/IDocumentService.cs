using Microsoft.AspNetCore.Mvc;
using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface IDocumentService {

        public ObjectResult CreateDocument(DocumentBL document);
        public DocumentBL GetDocumentById(long Id);
    }

}