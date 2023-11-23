using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.Interfaces {

    public interface IDocumentTypeLogic {
        public List<DocumentType> GetDocumentTypes(int? page, bool? fullPerms);
        public BusinessLogicResult<DocumentType> CreateDocumentType(DocumentType documentType);
        public BusinessLogicResult<DocumentType> UpdateDocumentType(int id,  DocumentType documentType);
        public BusinessLogicResult DeleteDocumentType(int id);

    }

}