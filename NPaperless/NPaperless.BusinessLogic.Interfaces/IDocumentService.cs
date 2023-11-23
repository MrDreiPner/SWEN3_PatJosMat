using NPaperless.REST.DTOs;
using Microsoft.AspNetCore.Mvc;
using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface IDocumentService {

        public ObjectResult CreateDocument(Document request);
        public ObjectResult DeleteDocumentById(long Id);
        public Document GetDocumentById(long Id);
        public Document UpdateDocument(UpdateDocumentRequest request);
    }

}