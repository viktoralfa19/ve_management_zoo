using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZooService.Models
{
    /// <summary>
    /// Animal Class
    /// </summary>
    public partial class Animal
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the type of the identifier animal.
        /// </summary>
        /// <value>
        /// The type of the identifier animal.
        /// </value>
        public short IdAnimalType { get; set; }
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
        /// Gets or sets the date admission.
        /// </summary>
        /// <value>
        /// The date admission.
        /// </value>
        public DateTime DateAdmission { get; set; }
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public decimal Weight { get; set; }
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Gets or sets the date updated.
        /// </summary>
        /// <value>
        /// The date updated.
        /// </value>
        public DateTime? DateUpdated { get; set; }
        /// <summary>
        /// Gets or sets the user created.
        /// </summary>
        /// <value>
        /// The user created.
        /// </value>
        public Guid UserCreated { get; set; }
        /// <summary>
        /// Gets or sets the user updated.
        /// </summary>
        /// <value>
        /// The user updated.
        /// </value>
        public Guid? UserUpdated { get; set; }
        /// <summary>
        /// Gets or sets the type of the animal.
        /// </summary>
        /// <value>
        /// The type of the animal.
        /// </value>
        [ForeignKey("IdSuscripcion")]
        public virtual AnimalType AnimalType { get; set; }

    }
}
