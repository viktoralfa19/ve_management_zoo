using System;

namespace SecurityDto
{
    /// <summary>
    /// DTo for resposne error into API
    /// </summary>
    public class ResponseErrorDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseErrorDto"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="message">The message.</param>
        /// <param name="id">The identifier.</param>
        public ResponseErrorDto(int status, string message, Guid? id = null)
        {
            Status = status;
            Message = id != null ? string.Concat(message, " Contact the administrator. Error code: ", id.ToString()) : message;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Status.
        /// </value>
        public int Status { get; set; }
        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        /// <value>
        /// The Message.
        /// </value>
        public string Message { get; set; }
    }
}
