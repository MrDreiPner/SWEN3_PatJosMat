using NPaperless.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Interfaces
{
    public interface IElastic
    {
        void AddDocumentAsync(ElasticDocument doc);
        IEnumerable<ElasticDocument> SearchDocumentAsync(string searchTerm);
    }
}
