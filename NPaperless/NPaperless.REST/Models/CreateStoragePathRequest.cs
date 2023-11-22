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
    public partial class CreateStoragePathRequest 
    {
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Path
        /// </summary>
        [Required]
        [DataMember(Name="path", EmitDefaultValue=false)]
        public string Path { get; set; }

        /// <summary>
        /// Gets or Sets MatchingAlgorithm
        /// </summary>
        [Required]
        [DataMember(Name="matching_algorithm", EmitDefaultValue=true)]
        public int MatchingAlgorithm { get; set; }

        /// <summary>
        /// Gets or Sets Match
        /// </summary>
        [Required]
        [DataMember(Name="match", EmitDefaultValue=false)]
        public string Match { get; set; }

        /// <summary>
        /// Gets or Sets IsInsensitive
        /// </summary>
        [Required]
        [DataMember(Name="is_insensitive", EmitDefaultValue=true)]
        public bool IsInsensitive { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [Required]
        [DataMember(Name="owner", EmitDefaultValue=true)]
        public int Owner { get; set; }

    }
}
