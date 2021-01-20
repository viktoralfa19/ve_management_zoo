
namespace SecurityDto
{
    /// <summary>
    /// App Settings
    /// </summary>
    public class AppSettingsDto
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }
        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { get; set; }
        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; }
        /// <summary>
        /// Gets or sets the expires days.
        /// </summary>
        /// <value>
        /// The expires days.
        /// </value>
        public int ExpiresDays { get; set; }
        /// <summary>
        /// Gets or sets the URL server swagger.
        /// </summary>
        /// <value>
        /// The URL server swagger.
        /// </value>
        public string UrlServerSwagger { get; set; }
    }
}
