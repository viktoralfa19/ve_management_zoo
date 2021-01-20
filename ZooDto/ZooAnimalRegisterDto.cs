using System;
using System.ComponentModel.DataAnnotations;

namespace ZooDto
{
    /// <summary>
    /// Register animal's information
    /// </summary>
    public class ZooAnimalRegisterDto
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the date admission.
        /// </summary>
        /// <value>
        /// The date admission.
        /// </value>
        [Required]
        public DateTime DateAdmission { get; set; }
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [Required]
        public decimal Weight { get; set; }
        /// <summary>
        /// Gets or sets the type of the identifier animal.
        /// </summary>
        /// <value>
        /// The type of the identifier animal.
        /// </value>
        [Required]
        public short IdAnimalType { get; set; }
    }
}
