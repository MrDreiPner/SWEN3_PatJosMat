/*
 * Paperless Rest Server
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using NPaperless.REST.Attributes;
using NPaperless.REST;
using log4net;
using NPaperless.BusinessLogic.Services;
using NPaperless.BusinessLogic.Interfaces;
using System.IO.Pipelines;
using System.Linq;
using Minio;

namespace NPaperless.REST.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class SearchApiController : ControllerBase
    { 
        private readonly ILog _logger = LogManager.GetLogger(typeof(SearchApiController));
        private readonly IElastic _elastic;

        public SearchApiController(IElastic elastic)
        {
            _elastic = elastic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <param name="limit"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/api/search/autocomplete")]
        [ValidateModelState]
        [SwaggerOperation("AutoComplete")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<string>), description: "Success")]
        public virtual IActionResult AutoComplete([FromQuery (Name = "term")]string term, [FromQuery (Name = "limit")]int? limit)
        {
            try
            {
                _logger.Info("got search request with search term: " + term);
                var searchResult = _elastic.SearchDocumentAsync(term);
                string responseResult = "Found documents:\n";
                if (searchResult != null)
                {
                    foreach (var doc in searchResult)
                    {
                        responseResult += (doc.Title + ": " + doc.Content);
                    }
                }
                return new ObjectResult(responseResult);
            }
            catch(Exception ex)
            {
                _logger.Info(ex + ex.Message);
                switch (ex)
                {
                    case ArgumentNullException:
                        _logger.Info("no searchterm given");
                        return new StatusCodeResult(500);
                    default:
                        return new ObjectResult("Error when searching for document");
                }        
            }
        }
    }
}
