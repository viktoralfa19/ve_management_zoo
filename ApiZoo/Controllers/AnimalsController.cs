using ApiZoo.Models;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZooDto;
using ZooService.Contracts.Manager;

namespace ApiZoo.Controllers
{
    /// <summary>
    /// Animal Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [SwaggerTag("Animal Group Endpoints")]
    [Route("api/v1")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AnimalsController : ControllerBase
    {
        #region Properties and Constructor          
        /// <summary>
        /// The imapper/
        /// </summary>
        private readonly IMapper _imapper;
        /// <summary>
        /// The animal service
        /// </summary>
        private IAnimalManager _animalService;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalsController"/> class.
        /// </summary>
        /// <param name="imapper">The imapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="animalService">The animal service.</param>
        public AnimalsController(IMapper imapper, ILogger<AnimalsController> logger, IAnimalManager animalService)
        {
            _imapper = imapper;
            _logger = logger;
            _animalService = animalService;
        }
        #endregion

        #region GET
        /// <summary>
        /// Gets all animal's information asynchronous.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     Get /api/v1/Animals
        ///     
        /// </remarks>
        /// <returns>Return the animal's information.</returns>
        /// <response code="200">Succeeded.</response>
        /// <response code="204">Succeeded but not found elements.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet("[controller]")]
        [EnableQuery(PageSize = 50)]
        [SwaggerResponse(200, Type = typeof(ResponseCollectionDto<ZooAnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseErrorDto))]
        [SwaggerResponse(500, Type = typeof(ResponseErrorDto))]
        public async Task<IActionResult> GetAllAnimalsByAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var animals = await _animalService.GetAllAnimalsAsync();
                if (animals == null || !animals.Any())
                    return NoContent();
                return Ok(new ResponseCollectionDto<ZooAnimalDto>((int)HttpStatusCode.OK, "Ok", animals));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to find animal information.", guid));
            }
        }

        /// <summary>
        /// Gets all animal's information asynchronous.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     Get /api/v1/animals/type/1
        ///     
        /// </remarks>
        /// /// <param name="id">The type animal.</param>
        /// <returns>Return the animal's information.</returns>
        /// <response code="200">Succeeded.</response>
        /// <response code="204">Succeeded but not found elements.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet("[controller]/type/{id}")]
        [EnableQuery(PageSize = 50)]
        [SwaggerResponse(200, Type = typeof(ResponseCollectionDto<ZooAnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseErrorDto))]
        [SwaggerResponse(500, Type = typeof(ResponseErrorDto))]
        public async Task<IActionResult> GetAllAnimalsByTypeAsync([FromRoute][Required] short id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var animals = await _animalService.GetAllAnimalsByTypeAnimalAsync(id);
                if (animals == null || !animals.Any())
                    return NoContent();
                return Ok(new ResponseCollectionDto<ZooAnimalDto>((int)HttpStatusCode.OK, "Ok", animals));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to find animal information.", guid));
            }
        }


        /// <summary>
        /// Gets one animal's information asynchronous.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     Get /api/v1/animals/1
        ///     
        /// </remarks>
        /// <returns>Return one animal's information.</returns>
        /// <response code="200">Succeeded.</response>
        /// <response code="204">Succeeded but not found elements.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        /// <param name="id">The identifier.</param>
        /// <returns>Return one animal information</returns>
        [HttpGet("[controller]/{id}")]
        [SwaggerResponse(200, Type = typeof(ResponseDto<ZooAnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseErrorDto))]
        [SwaggerResponse(500, Type = typeof(ResponseErrorDto))]
        public async Task<IActionResult> GetAnimalByIdAsync([FromRoute][Required] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var animal = await _animalService.GetAnimalByIdAsync(id);
                if (animal == null)
                    return NoContent();
                return Ok(new ResponseDto<ZooAnimalDto>((int)HttpStatusCode.OK, "Ok", animal));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to find animal information.", guid));
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Creates the animal of Zoo asynchronous.
        /// </summary>
        /// <param name="obj">The animal's information.</param>
        /// <returns>Return status created</returns>
        /// <response code="201">Succeeded Created.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [Authorize(Roles = "Administrador")]
        [HttpPost("[controller]")]
        public async Task<IActionResult> CreatePositionAsync([FromBody] ZooAnimalRegisterDto obj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var created = await _animalService.CreateAnimalAsync(obj,User.GetIdUser());
                return CreatedAtAction(nameof(GetAnimalByIdAsync).Replace("Async", string.Empty), new { id = created }, new ResponseDto<ZooAnimalRegisterDto>((int)HttpStatusCode.Created, "Ok", obj));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to create the animal information.", guid));
            }
        }
        #endregion

        #region PUT
        /// <summary>
        /// Updates the animal of Zoo asynchronous.
        /// </summary>
        /// <param name="id">The animal's id.</param>
        /// <param name="obj">The animal's information.</param>
        /// <returns>Return status updated</returns>
        /// <response code="200">Succeeded Updted.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [Authorize(Roles = "Administrador")]
        [HttpPut("[controller]/{id}")]
        public async Task<IActionResult> UpdatePositionAsync([FromRoute][Required] int id,[FromBody] ZooAnimalRegisterDto obj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                var updated = await _animalService.UpdateAnimalAsync(id, obj, User.GetIdUser());
                return Ok(new ResponseDto<ZooAnimalRegisterDto>((int)HttpStatusCode.OK, "Ok", obj));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to create the animal information.", guid));
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete the animal of Zoo asynchronous.
        /// </summary>
        /// <param name="id">The animal's id.</param>
        /// <returns>Return status delete</returns>
        /// <response code="200">Succeeded Deleted.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [Authorize(Roles = "Administrador")]
        [HttpDelete("[controller]/{id}")]
        public async Task<IActionResult> DeletePositionAsync([FromRoute][Required] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters"));
                await _animalService.DeleteAnimalAsync(id);
                return Ok(new ResponseDto<string>((int)HttpStatusCode.OK, "Ok"));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to create the animal information.", guid));
            }
        }
        #endregion

        #region PATH
        /// <summary>
        /// Updates Partial the animal of Zoo asynchronous.
        /// </summary>
        /// <param name="id">The animal's id.</param>
        /// <param name="patchObj">The animal's partial information.</param>
        /// <returns>Return status update partial</returns>
        /// <response code="200">Succeeded Partial Updated.</response>
        /// <response code="400">Bad Request.</response> 
        /// <response code="401">Unauthorized.</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="405">Method Not Allowed.</response> 
        /// <response code="500">Internal Server Error.</response> 
        [Authorize(Roles = "Administrador")]
        [HttpPatch("[controller]/{id}")]
        public async Task<IActionResult> PatchPositionAsync([FromRoute][Required] int id, [FromBody] JsonPatchDocument<ZooAnimalRegisterDto> patchObj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Review Required Parameters."));
                var animal = await _animalService.GetAnimalByIdAsync(id);
                if (animal == null)
                    return StatusCode((int)HttpStatusCode.NotFound,new ResponseDto<string>((int)HttpStatusCode.NotFound, "The animal does not exist to update its information."));
                var animalRegister = _imapper.Map<ZooAnimalDto,ZooAnimalRegisterDto>(animal);
                patchObj.ApplyTo(animalRegister);
                var updated = await _animalService.UpdateAnimalAsync(id, animalRegister, User.GetIdUser());
                return Ok(new ResponseDto<ZooAnimalRegisterDto>((int)HttpStatusCode.OK, "Ok", animalRegister));
            }
            catch (ExceptionDto exdto)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseErrorDto((int)HttpStatusCode.InternalServerError, exdto.UserMessage, exdto.Id));
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseErrorDto((int)HttpStatusCode.BadRequest, "Failed to update the animal information.", guid));
            }
        }
        #endregion
    }
}
