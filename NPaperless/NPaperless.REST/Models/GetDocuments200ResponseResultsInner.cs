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
    public partial class GetDocuments200ResponseResultsInner 
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name="id", EmitDefaultValue=true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Correspondent
        /// </summary>
        [Required]
        [DataMember(Name="correspondent", EmitDefaultValue=true)]
        public int Correspondent { get; set; }

        /// <summary>
        /// Gets or Sets DocumentType
        /// </summary>
        [Required]
        [DataMember(Name="document_type", EmitDefaultValue=true)]
        public int DocumentType { get; set; }

        /// <summary>
        /// Gets or Sets StoragePath
        /// </summary>
        [Required]
        [DataMember(Name="storage_path", EmitDefaultValue=true)]
        public int StoragePath { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [Required]
        [DataMember(Name="title", EmitDefaultValue=false)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        [Required]
        [DataMember(Name="content", EmitDefaultValue=false)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Tags
        /// </summary>
        [Required]
        [DataMember(Name="tags", EmitDefaultValue=false)]
        public List<int> Tags { get; set; }

        /// <summary>
        /// Gets or Sets Created
        /// </summary>
        [Required]
        [DataMember(Name="created", EmitDefaultValue=false)]
        public string Created { get; set; }

        /// <summary>
        /// Gets or Sets CreatedDate
        /// </summary>
        [Required]
        [DataMember(Name="created_date", EmitDefaultValue=false)]
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets Modified
        /// </summary>
        [Required]
        [DataMember(Name="modified", EmitDefaultValue=false)]
        public string Modified { get; set; }

        /// <summary>
        /// Gets or Sets Added
        /// </summary>
        [Required]
        [DataMember(Name="added", EmitDefaultValue=false)]
        public string Added { get; set; }

        /// <summary>
        /// Gets or Sets ArchiveSerialNumber
        /// </summary>
        [Required]
        [DataMember(Name="archive_serial_number", EmitDefaultValue=true)]
        public int ArchiveSerialNumber { get; set; }

        /// <summary>
        /// Gets or Sets OriginalFileName
        /// </summary>
        [Required]
        [DataMember(Name="original_file_name", EmitDefaultValue=false)]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or Sets ArchivedFileName
        /// </summary>
        [Required]
        [DataMember(Name="archived_file_name", EmitDefaultValue=false)]
        public string ArchivedFileName { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [Required]
        [DataMember(Name="owner", EmitDefaultValue=true)]
        public int Owner { get; set; }

        /// <summary>
        /// Gets or Sets UserCanChange
        /// </summary>
        [Required]
        [DataMember(Name="user_can_change", EmitDefaultValue=true)]
        public bool UserCanChange { get; set; }

        /// <summary>
        /// Gets or Sets Notes
        /// </summary>
        [Required]
        [DataMember(Name="notes", EmitDefaultValue=false)]
        public List<GetDocuments200ResponseResultsInnerNotesInner> Notes { get; set; }

    }
}
