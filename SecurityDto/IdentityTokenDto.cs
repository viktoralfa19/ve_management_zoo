using System;
/// <summary>
/// Security
/// </summary>
namespace SecurityDto
{
    /// <summary>
    /// Identity Token Class
    /// </summary>
    public class IdentityTokenDto
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the validate from.
        /// </summary>
        /// <value>
        /// The validate from.
        /// </value>
        public DateTime? ValidateFrom { get; set; }
        /// <summary>
        /// Gets or sets the validate to.
        /// </summary>
        /// <value>
        /// The validate to.
        /// </value>
        public DateTime? ValidateTo { get; set; }
    }
}
