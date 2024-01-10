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
    public partial class GetDocumentMetadata200Response 
    {
        /// <summary>
        /// Gets or Sets OriginalChecksum
        /// </summary>
        [Required]
        [DataMember(Name="original_checksum", EmitDefaultValue=false)]
        public string OriginalChecksum { get; set; }

        /// <summary>
        /// Gets or Sets OriginalSize
        /// </summary>
        [Required]
        [DataMember(Name="original_size", EmitDefaultValue=true)]
        public int OriginalSize { get; set; }

        /// <summary>
        /// Gets or Sets OriginalMimeType
        /// </summary>
        [Required]
        [DataMember(Name="original_mime_type", EmitDefaultValue=false)]
        public string OriginalMimeType { get; set; }

        /// <summary>
        /// Gets or Sets MediaFilename
        /// </summary>
        [Required]
        [DataMember(Name="media_filename", EmitDefaultValue=false)]
        public string MediaFilename { get; set; }

        /// <summary>
        /// Gets or Sets HasArchiveVersion
        /// </summary>
        [Required]
        [DataMember(Name="has_archive_version", EmitDefaultValue=true)]
        public bool HasArchiveVersion { get; set; }

        /// <summary>
        /// Gets or Sets OriginalMetadata
        /// </summary>
        [Required]
        [DataMember(Name="original_metadata", EmitDefaultValue=false)]
        public List<Object> OriginalMetadata { get; set; }

        /// <summary>
        /// Gets or Sets ArchiveChecksum
        /// </summary>
        [Required]
        [DataMember(Name="archive_checksum", EmitDefaultValue=false)]
        public string ArchiveChecksum { get; set; }

        /// <summary>
        /// Gets or Sets ArchiveMediaFilename
        /// </summary>
        [Required]
        [DataMember(Name="archive_media_filename", EmitDefaultValue=false)]
        public string ArchiveMediaFilename { get; set; }

        /// <summary>
        /// Gets or Sets OriginalFilename
        /// </summary>
        [Required]
        [DataMember(Name="original_filename", EmitDefaultValue=false)]
        public string OriginalFilename { get; set; }

        /// <summary>
        /// Gets or Sets Lang
        /// </summary>
        [Required]
        [DataMember(Name="lang", EmitDefaultValue=false)]
        public string Lang { get; set; }

        /// <summary>
        /// Gets or Sets ArchiveSize
        /// </summary>
        [Required]
        [DataMember(Name="archive_size", EmitDefaultValue=true)]
        public int ArchiveSize { get; set; }

        /// <summary>
        /// Gets or Sets ArchiveMetadata
        /// </summary>
        [Required]
        [DataMember(Name="archive_metadata", EmitDefaultValue=false)]
        public List<GetDocumentMetadata200ResponseArchiveMetadataInner> ArchiveMetadata { get; set; }

    }
}
