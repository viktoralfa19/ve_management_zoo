using System;

namespace ZooDto
{
    /// <summary>
    /// Custom Exception Class
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ExceptionDto : Exception
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private Guid _id;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != null)
                {
                    _id = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the mensaje usuario.
        /// </summary>
        /// <value>
        /// The mensaje usuario.
        /// </value>
        public string UserMessage { get; set; }

        /// <summary>
        /// Determines whether [is null identifier].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is null identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNullId()
        {
            return _id == null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDto"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="mensaje">The mensaje.</param>
        public ExceptionDto(Guid id, string mensaje) : base(mensaje)
        {
            _id = id;
            UserMessage = mensaje;
        }
    }
}
