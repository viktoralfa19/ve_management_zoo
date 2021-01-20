using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ApiZoo.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserExtend
    {
        /// <summary>
        /// Gets the identifier user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static Guid GetIdUser(this IPrincipal user)
        {
            Claim claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                return new Guid();
            var dato = new Guid(claim.Value);
            return dato;
        }
    }
}
