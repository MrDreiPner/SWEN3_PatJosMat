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
using NPaperless.BusinessLogic.Validators;
using Microsoft.AspNetCore.Mvc;

namespace NPaperless.BusinessLogic.ElasticSearch
{
    public class ElasticSearch : IElastic
    {
        private readonly Uri _uri;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OcrBackgroundService));
        private readonly SearchTermValidator _termValidator;

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
            var validationResult = _termValidator.Validate(searchTerm);

            if (!validationResult.IsValid)
            {
                _logger.Info("search term is not valid");
                throw new ArgumentNullException();
            }
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
