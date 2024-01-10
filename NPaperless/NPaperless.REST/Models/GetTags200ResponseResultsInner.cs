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
    public partial class GetTags200ResponseResultsInner 
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name="id", EmitDefaultValue=true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Slug
        /// </summary>
        [Required]
        [DataMember(Name="slug", EmitDefaultValue=false)]
        public string Slug { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Color
        /// </summary>
        [Required]
        [DataMember(Name="color", EmitDefaultValue=false)]
        public string Color { get; set; }

        /// <summary>
        /// Gets or Sets TextColor
        /// </summary>
        [Required]
        [DataMember(Name="text_color", EmitDefaultValue=false)]
        public string TextColor { get; set; }

        /// <summary>
        /// Gets or Sets Match
        /// </summary>
        [Required]
        [DataMember(Name="match", EmitDefaultValue=false)]
        public string Match { get; set; }

        /// <summary>
        /// Gets or Sets MatchingAlgorithm
        /// </summary>
        [Required]
        [DataMember(Name="matching_algorithm", EmitDefaultValue=true)]
        public int MatchingAlgorithm { get; set; }

        /// <summary>
        /// Gets or Sets IsInsensitive
        /// </summary>
        [Required]
        [DataMember(Name="is_insensitive", EmitDefaultValue=true)]
        public bool IsInsensitive { get; set; }

        /// <summary>
        /// Gets or Sets IsInboxTag
        /// </summary>
        [Required]
        [DataMember(Name="is_inbox_tag", EmitDefaultValue=true)]
        public bool IsInboxTag { get; set; }

        /// <summary>
        /// Gets or Sets DocumentCount
        /// </summary>
        [Required]
        [DataMember(Name="document_count", EmitDefaultValue=true)]
        public int DocumentCount { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [Required]
        [DataMember(Name="owner", EmitDefaultValue=true)]
        public int Owner { get; set; }

        /// <summary>
        /// Gets or Sets Permissions
        /// </summary>
        [Required]
        [DataMember(Name="permissions", EmitDefaultValue=false)]
        public GetCorrespondents200ResponseResultsInnerPermissions Permissions { get; set; }

    }
}
