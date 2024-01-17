using Microsoft.AspNetCore.Mvc;
using NPaperless.BusinessLogic.Entities;
using System.Net;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface IDocumentService {

        public Task<HttpStatusCode> CreateDocument(DocumentBL document);
        public DocumentBL GetDocumentById(long Id);
    }

}