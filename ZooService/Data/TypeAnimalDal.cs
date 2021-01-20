using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZooDto;
using ZooService.Contracts.Repository;
using ZooService.Models;

namespace ZooService.Data
{
    /// <summary>
    /// Access data of Type Animal
    /// </summary>
    public class TypeAnimalDal
    {
        #region Propierties and Constructor                
        /// <summary>
        /// The imapper
        /// </summary>
        private readonly IMapper _imapper;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The get animal type repository
        /// </summary>
        private readonly IGetData<AnimalType> _getAnimalTypeRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAnimalDal"/> class.
        /// </summary>
        /// <param name="imapper">The imapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="getAnimalTypeRepository">The get animal type repository.</param>
        public TypeAnimalDal(IMapper imapper, ILogger logger, IGetData<AnimalType> getAnimalTypeRepository)
        {
            _imapper = imapper;
            _logger = logger;
            _getAnimalTypeRepository = getAnimalTypeRepository;
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
                ICollection<ZooTypeAnimalDto> lstAnimaslDto = new List<ZooTypeAnimalDto>();
                var query = _getAnimalTypeRepository.Get();
                var lstTypeAnimals = await query.ToListAsync();
                lstAnimaslDto = _imapper.Map<ICollection<AnimalType>, ICollection<ZooTypeAnimalDto>>(lstTypeAnimals);
                return lstAnimaslDto;
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
