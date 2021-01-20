using SecurityDto;
using System.Threading.Tasks;

namespace SecurityService.Contracts
{
    /// <summary>
    /// Interface Application User
    /// </summary>
    public interface IApplicationUserManager
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <returns></returns>
        Task<IdentityTokenDto> GetToken(CredentialsDto user, AppSettingsDto appSettings);
    }
}
