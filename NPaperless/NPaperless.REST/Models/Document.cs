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
using Microsoft.AspNetCore.Http;

namespace NPaperless.REST
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Document 
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Correspondent
        /// </summary>
        [DataMember(Name="correspondent", EmitDefaultValue=true)]
        public int? Correspondent { get; set; }

        /// <summary>
        /// Gets or Sets DocumentType
        /// </summary>
        [DataMember(Name="document_type", EmitDefaultValue=true)]
        public int? DocumentType { get; set; }

        /// <summary>
        /// Gets or Sets StoragePath
        /// </summary>
        [DataMember(Name="storage_path", EmitDefaultValue=true)]
        public int? StoragePath { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name="title", EmitDefaultValue=true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        [DataMember(Name="content", EmitDefaultValue=true)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Tags
        /// </summary>
        [DataMember(Name="tags", EmitDefaultValue=true)]
        public List<int> Tags { get; set; }

        /// <summary>
        /// Gets or Sets Created
        /// </summary>
        [DataMember(Name="created", EmitDefaultValue=false)]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or Sets CreatedDate
        /// </summary>
        [DataMember(Name="created_date", EmitDefaultValue=false)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets Modified
        /// </summary>
        [DataMember(Name="modified", EmitDefaultValue=false)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or Sets Added
        /// </summary>
        [DataMember(Name="added", EmitDefaultValue=false)]
        public DateTime Added { get; set; }

        /// <summary>
        /// Gets or Sets ArchiveSerialNumber
        /// </summary>
        [DataMember(Name="archive_serial_number", EmitDefaultValue=true)]
        public string ArchiveSerialNumber { get; set; }

        /// <summary>
        /// Gets or Sets OriginalFileName
        /// </summary>
        [DataMember(Name="original_file_name", EmitDefaultValue=true)]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or Sets ArchivedFileName
        /// </summary>
        [DataMember(Name="archived_file_name", EmitDefaultValue=true)]
        public string ArchivedFileName { get; set; }

        /// <summary>
        /// Gets or Sets UploadDocument
        /// </summary>
        [DataMember(Name="upload_document", EmitDefaultValue=true)]
        public IFormFile UploadDocument { get; set; }
    }
}
