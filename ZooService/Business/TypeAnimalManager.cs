using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZooDto;
using ZooService.Contracts.Manager;
using ZooService.Contracts.Repository;
using ZooService.Data;
using ZooService.Models;

namespace ZooService.Business
{
    /// <summary>
    /// Manager for types animals data
    /// </summary>
    public class TypeAnimalManager : ITypeAnimalManager
    {
        #region Properties and Constructor
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The type animal dal
        /// </summary>
        private readonly TypeAnimalDal _typeAnimalDal;
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAnimalManager"/> class.
        /// </summary>
        /// <param name="imapper">The imapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="getTypeAnimalRepository">The get type animal repository.</param>
        public TypeAnimalManager(IMapper imapper, ILogger<AnimalManager> logger, IGetData<AnimalType> getTypeAnimalRepository)
        {
            _logger = logger;
            _typeAnimalDal = new TypeAnimalDal(imapper, logger, getTypeAnimalRepository);
        }
        #endregion

        #region GET                        
        /// <summary>
        /// Gets all type animals asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">There was an error getting all the animals data.</exception>
        public async Task<ICollection<ZooTypeAnimalDto>> GetAllTypeAnimalsAsync()
        {
            try
            {
                return await _typeAnimalDal.GetAllTypeAnimalsAsync();
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting all the types of animals data.");
            }
        }
        #endregion
    }
}
