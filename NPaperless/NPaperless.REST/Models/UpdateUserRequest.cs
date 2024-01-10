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
    public partial class UpdateUserRequest 
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name="id", EmitDefaultValue=true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        [Required]
        [DataMember(Name="username", EmitDefaultValue=false)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        [Required]
        [DataMember(Name="email", EmitDefaultValue=false)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        [Required]
        [DataMember(Name="password", EmitDefaultValue=false)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        [Required]
        [DataMember(Name="first_name", EmitDefaultValue=false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        [Required]
        [DataMember(Name="last_name", EmitDefaultValue=false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets DateJoined
        /// </summary>
        [Required]
        [DataMember(Name="date_joined", EmitDefaultValue=false)]
        public string DateJoined { get; set; }

        /// <summary>
        /// Gets or Sets IsStaff
        /// </summary>
        [Required]
        [DataMember(Name="is_staff", EmitDefaultValue=true)]
        public bool IsStaff { get; set; }

        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        [Required]
        [DataMember(Name="is_active", EmitDefaultValue=true)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or Sets IsSuperuser
        /// </summary>
        [Required]
        [DataMember(Name="is_superuser", EmitDefaultValue=true)]
        public bool IsSuperuser { get; set; }

        /// <summary>
        /// Gets or Sets Groups
        /// </summary>
        [Required]
        [DataMember(Name="groups", EmitDefaultValue=false)]
        public List<Object> Groups { get; set; }

        /// <summary>
        /// Gets or Sets UserPermissions
        /// </summary>
        [Required]
        [DataMember(Name="user_permissions", EmitDefaultValue=false)]
        public List<Object> UserPermissions { get; set; }

        /// <summary>
        /// Gets or Sets InheritedPermissions
        /// </summary>
        [Required]
        [DataMember(Name="inherited_permissions", EmitDefaultValue=false)]
        public List<string> InheritedPermissions { get; set; }

        /// <summary>
        /// Gets or Sets PermissionsForm
        /// </summary>
        [Required]
        [DataMember(Name="permissions_form", EmitDefaultValue=false)]
        public Object PermissionsForm { get; set; }

    }
}
