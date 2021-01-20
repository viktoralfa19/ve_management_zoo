using System;
using System.Collections.Generic;

namespace ZooDto
{
    /// <summary>
    /// DTO for response data from API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseCollectionDto<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseDto{T}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <param name="id">The identifier.</param>
        public ResponseCollectionDto(int status, string message, ICollection<T> data = null, Guid? id = null)
        {
            this.status = status;
            this.message = id != null ? String.Concat(message, " Contact the administrator. Error code: ", id.ToString()) : message;
            this.data = data;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string message { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public ICollection<T> data { get; set; }
    }
}
