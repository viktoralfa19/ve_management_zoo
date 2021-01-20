using System.Collections.Generic;

namespace ZooService.Models
{
    /// <summary>
    /// Animal Type Class
    /// </summary>
    public partial class AnimalType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalType"/> class.
        /// </summary>
        public AnimalType()
        {
            Animals = new HashSet<Animal>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the animals.
        /// </summary>
        /// <value>
        /// The animals.
        /// </value>
        public virtual ICollection<Animal> Animals { get; set; } 
    }
}
