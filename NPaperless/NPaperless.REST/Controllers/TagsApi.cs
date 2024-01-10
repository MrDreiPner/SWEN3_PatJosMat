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
using NPaperless.BusinessLogic.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NPaperless.BusinessLogic.Interfaces;
using NPaperless.DataAccess.Entities;

namespace NPaperless.REST.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class TagsApiController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createTagRequest"></param>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("/api/tags")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("CreateTag")]
        [SwaggerResponse(statusCode: 200, type: typeof(CreateTag200Response), description: "Success")]
        public virtual IActionResult CreateTag([FromBody]CreateTagRequest createTagRequest)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(CreateTag200Response));
            string exampleJson = null;
            exampleJson = "{\n  \"owner\" : 1,\n  \"matching_algorithm\" : 6,\n  \"user_can_change\" : true,\n  \"color\" : \"color\",\n  \"is_insensitive\" : true,\n  \"name\" : \"name\",\n  \"match\" : \"match\",\n  \"id\" : 0,\n  \"text_color\" : \"text_color\",\n  \"is_inbox_tag\" : true,\n  \"slug\" : \"slug\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CreateTag200Response>(exampleJson)
            : default(CreateTag200Response);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Success</response>
        [HttpDelete]
        [Route("/api/tags/{id}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteTag")]
        public virtual IActionResult DeleteTag([FromRoute (Name = "id")][Required]int id)
        {

            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fullPerms"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/api/tags")]
        [ValidateModelState]
        [SwaggerOperation("GetTags")]
        [SwaggerResponse(statusCode: 200, type: typeof(GetTags200Response), description: "Success")]
        public virtual IActionResult GetTags([FromQuery (Name = "page")]int? page, [FromQuery (Name = "full_perms")]bool? fullPerms)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(GetTags200Response));
            string exampleJson = null;
            exampleJson = "{\n  \"next\" : 6,\n  \"all\" : [ 5, 5 ],\n  \"previous\" : 1,\n  \"count\" : 0,\n  \"results\" : [ {\n    \"owner\" : 9,\n    \"matching_algorithm\" : 2,\n    \"document_count\" : 7,\n    \"color\" : \"color\",\n    \"is_insensitive\" : true,\n    \"permissions\" : {\n      \"view\" : {\n        \"groups\" : [ \"\", \"\" ],\n        \"users\" : [ \"\", \"\" ]\n      },\n      \"change\" : {\n        \"groups\" : [ \"\", \"\" ],\n        \"users\" : [ \"\", \"\" ]\n      }\n    },\n    \"name\" : \"name\",\n    \"match\" : \"match\",\n    \"id\" : 5,\n    \"text_color\" : \"text_color\",\n    \"is_inbox_tag\" : true,\n    \"slug\" : \"slug\"\n  }, {\n    \"owner\" : 9,\n    \"matching_algorithm\" : 2,\n    \"document_count\" : 7,\n    \"color\" : \"color\",\n    \"is_insensitive\" : true,\n    \"permissions\" : {\n      \"view\" : {\n        \"groups\" : [ \"\", \"\" ],\n        \"users\" : [ \"\", \"\" ]\n      },\n      \"change\" : {\n        \"groups\" : [ \"\", \"\" ],\n        \"users\" : [ \"\", \"\" ]\n      }\n    },\n    \"name\" : \"name\",\n    \"match\" : \"match\",\n    \"id\" : 5,\n    \"text_color\" : \"text_color\",\n    \"is_inbox_tag\" : true,\n    \"slug\" : \"slug\"\n  } ]\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<GetTags200Response>(exampleJson)
            : default(GetTags200Response);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTagRequest"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/api/tags/{id}")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("UpdateTag")]
        [SwaggerResponse(statusCode: 200, type: typeof(UpdateTag200Response), description: "Success")]
        public virtual IActionResult UpdateTag([FromRoute (Name = "id")][Required]int id, [FromBody]UpdateTagRequest updateTagRequest)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(UpdateTag200Response));
            string exampleJson = null;
            exampleJson = "{\n  \"owner\" : 5,\n  \"matching_algorithm\" : 6,\n  \"user_can_change\" : true,\n  \"document_count\" : 1,\n  \"color\" : \"color\",\n  \"is_insensitive\" : true,\n  \"name\" : \"name\",\n  \"match\" : \"match\",\n  \"id\" : 0,\n  \"text_color\" : \"text_color\",\n  \"is_inbox_tag\" : true,\n  \"slug\" : \"slug\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<UpdateTag200Response>(exampleJson)
            : default(UpdateTag200Response);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
