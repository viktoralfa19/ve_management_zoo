using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityService.Models
{
    /// <summary>
    /// Application User - Role
    /// </summary>
    /// <seealso cref="IdentityUserRole{Guid}" />
    class ApplicationUserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public override Guid UserId { get; set; }
        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public override Guid RoleId { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual ApplicationUser User { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public virtual ApplicationRole Role { get; set; }
    }
}
