using Microsoft.AspNetCore.Identity;
using System;

namespace SecurityService.Models
{
    /// <summary>
    /// Application Role
    /// </summary>
    /// <seealso cref="IdentityRole{Guid}" />
    public class ApplicationRole: IdentityRole<Guid>
    {
    }
}
