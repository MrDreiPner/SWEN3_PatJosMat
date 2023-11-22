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
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NPaperless.REST.Converters;

namespace NPaperless.REST.DTOs
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class GetStoragePaths200ResponseResultsInnerPermissions 
    {
        /// <summary>
        /// Gets or Sets View
        /// </summary>
        [Required]
        [DataMember(Name="view", EmitDefaultValue=false)]
        public GetStoragePaths200ResponseResultsInnerPermissionsView View { get; set; }

        /// <summary>
        /// Gets or Sets Change
        /// </summary>
        [Required]
        [DataMember(Name="change", EmitDefaultValue=false)]
        public GetStoragePaths200ResponseResultsInnerPermissionsView Change { get; set; }

    }
}
