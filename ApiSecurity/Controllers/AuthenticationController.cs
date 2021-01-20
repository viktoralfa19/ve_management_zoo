using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SecurityDto;
using SecurityService.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ApiSecurity.Controllers
{
    /// <summary>
    /// Autentication Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [SwaggerTag("Token Generate")]
    [Route("api/v1")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Properties and Constructor
        /// <summary>
        /// The user service
        /// </summary>
        private IApplicationUserManager _userService;
        /// <summary>
        /// The application settings
        /// </summary>
        private readonly AppSettingsDto _appSettings;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="logger">The logger.</param>
        public AuthenticationController(IOptions<AppSettingsDto> appSettings, IApplicationUserManager userService, ILogger<AuthenticationController> logger)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
            _logger = logger;
        }
        #endregion

        #region POST
        /// <summary>
        /// Asynchronous User authentication.
        /// </summary>
        /// <param name="credencials">The credenciales.</param>
        /// <returns>Return token created</returns>
        /// <response code="201">Succeeded Created.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [AllowAnonymous]
        [HttpPost("[controller]")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] CredentialsDto credencials)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var userToken = await _userService.GetToken(credencials, _appSettings);
                return CreatedAtAction(nameof(AuthenticateAsync).Replace("Async", string.Empty), null, new ResponseDto<IdentityTokenDto>((int)HttpStatusCode.Created, "Ok", userToken));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "The user could not be authenticated.", guid));
            }
        }
        #endregion
    }
}