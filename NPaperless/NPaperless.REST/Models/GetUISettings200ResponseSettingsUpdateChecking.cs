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

namespace NPaperless.REST
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class GetUISettings200ResponseSettingsUpdateChecking 
    {
        /// <summary>
        /// Gets or Sets BackendSetting
        /// </summary>
        [Required]
        [DataMember(Name="backend_setting", EmitDefaultValue=false)]
        public string BackendSetting { get; set; }

    }
}
