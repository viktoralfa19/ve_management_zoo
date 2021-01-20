﻿using System.ComponentModel.DataAnnotations;
/// <summary>
/// Security
/// </summary>
namespace SecurityDto
{
    /// <summary>
    /// Credentials Class
    /// </summary>
    public class CredentialsDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
