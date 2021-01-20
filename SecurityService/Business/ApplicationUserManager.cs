using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SecurityDto;
using SecurityService.Contracts;
using SecurityService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService.Business
{
    /// <summary>
    /// Security Generate Token Class
    /// </summary>
    /// <seealso cref="IApplicationUserManager" />
    public class ApplicationUserManager : IApplicationUserManager
    {
        #region Properties and Constructor
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The sign in manager
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="logger">The logger.</param>
        public ApplicationUserManager(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUserManager> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Finds the user asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">
        /// Does'n exist the user
        /// or
        /// There was an error searching for the user
        /// </exception>
        private async Task<ApplicationUser> FindUserAsync(string username, string password)
        {
            ApplicationUser user;
            try
            {
                user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    throw new ExceptionDto(Guid.NewGuid(), "Does'n exist the user");
                }
                return await _userManager.CheckPasswordAsync(user, password) ? user : null;
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error searching for the user");
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">
        /// Username or Password are incorrect.
        /// or
        /// There was an error generating the Authentication Token. The user could not be authenticated
        /// </exception>
        public async Task<IdentityTokenDto> GetToken(CredentialsDto model, AppSettingsDto appSettings)
        {
            try
            {
                var identityToken = new IdentityTokenDto();
                var appUser = await FindUserAsync(model.Username, model.Password);

                if (appUser == null)
                    throw new ExceptionDto(Guid.NewGuid(), "Username or Password are incorrect.");

                var userRoles = await _userManager.GetRolesAsync(appUser);

                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

                var listaClaims = new List<Claim>();
                listaClaims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
                listaClaims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                foreach (var rol in userRoles)
                {
                    listaClaims.Add(new Claim(ClaimTypes.Role, rol));
                }
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(listaClaims),
                    Expires = DateTime.UtcNow.AddDays(appSettings.ExpiresDays),
                    Audience = appSettings.Audience,
                    Issuer = appSettings.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                identityToken.Token = tokenHandler.WriteToken(token);
                identityToken.ValidateFrom = token.ValidFrom;
                identityToken.ValidateTo = token.ValidTo;

                return identityToken;
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error generating the Authentication Token. The user could not be authenticated");
            }
        }
        #endregion
    }
}
