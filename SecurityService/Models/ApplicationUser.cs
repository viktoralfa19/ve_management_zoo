using Microsoft.AspNetCore.Identity;
using System;

namespace SecurityService.Models
{
    /// <summary>
    /// Application User
    /// </summary>
    /// <seealso cref="IdentityUser{Guid}" />
    public class ApplicationUser: IdentityUser<Guid>
    {
    }
}
