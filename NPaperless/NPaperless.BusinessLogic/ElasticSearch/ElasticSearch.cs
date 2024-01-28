using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using NPaperless.BusinessLogic.Interfaces;
using log4net;
using NPaperless.BusinessLogic.Services;
using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.ElasticSearch
{
    public class ElasticSearch : IElastic
    {
        private readonly Uri _uri;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OcrBackgroundService));

        public ElasticSearch(IConfiguration configuration)
        {
            this._uri = new Uri(configuration.GetConnectionString("ElasticSearch") ?? "http://elasticsearch:9200/");
        }

        public void AddDocumentAsync(ElasticDocument document)
        {
            var elasticClient = new ElasticsearchClient(_uri);

            if (!elasticClient.Indices.Exists("documents").Exists)
                elasticClient.Indices.Create("documents");

            var indexResponse = elasticClient.Index(document, "documents");
            if (!indexResponse.IsSuccess())
            {
                // Handle errors
                _logger.Error($"Failed to index document: {indexResponse.DebugInformation}\n{indexResponse.ElasticsearchServerError}");

                throw new Exception($"Failed to index document: {indexResponse.DebugInformation}\n{indexResponse.ElasticsearchServerError}");
            }
        }

        public IEnumerable<ElasticDocument> SearchDocumentAsync(string searchTerm)
        {
            _logger.Debug("Passed searchterm -> " + searchTerm);
            var elasticClient = new ElasticsearchClient(_uri);

            var searchResponse = elasticClient.Search<ElasticDocument>(s => s
                .Index("documents")
                .Query(q => q.QueryString(qs => qs.DefaultField(p => p.Content).Query($"*{searchTerm}*")))
            );

            return searchResponse.Documents;
        }
    }

}
