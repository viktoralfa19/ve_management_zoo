using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZooDto;
using ZooService.Contracts.Manager;

namespace ApiZoo.Controllers
{
    /// <summary>
    /// Controller for types animals
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [SwaggerTag("Type Animals Group Endpoints")]
    [Route("api/v1")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class TypesAnimalsController : ControllerBase
    {
        #region Properties and Constructor          
        /// <summary>
        /// The animal service
        /// </summary>
        private ITypeAnimalManager _typeAnimalService;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="TypesAnimalsController"/> class.
        /// </summary>
        /// <param name="imapper">The imapper.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="typeAnimalService">The type animal service.</param>
        public TypesAnimalsController(ILogger<TypesAnimalsController> logger, ITypeAnimalManager typeAnimalService)
        {
            _logger = logger;
            _typeAnimalService = typeAnimalService;
        }
        #endregion

        #region GET
        /// <summary>
        /// Gets all types of animal's information asynchronous.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     Get /api/v1/TypesAnimals
        ///     
        /// </remarks>
        /// <returns>Return the all types of animals information.</returns>
        /// <response code="200">Succeeded.</response>
        /// <response code="204">Succeeded but not found elements.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet("[controller]")]
        [EnableQuery(PageSize = 50)]
        [SwaggerResponse(200, Type = typeof(ResponseCollectionDto<ZooTypeAnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseErrorDto))]
        [SwaggerResponse(500, Type = typeof(ResponseErrorDto))]
        public async Task<IActionResult> GetAllTypesAnimalsByAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var typesAnimals = await _typeAnimalService.GetAllTypeAnimalsAsync();
                if (typesAnimals == null || !typesAnimals.Any())
                    return NoContent();
                return Ok(new ResponseCollectionDto<ZooTypeAnimalDto>((int)HttpStatusCode.OK, "Ok", typesAnimals));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to find types of animals information.", guid));
            }
        }
        #endregion
    }
}
