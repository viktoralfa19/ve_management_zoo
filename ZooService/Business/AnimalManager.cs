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
    /// Manager class for Animal Entity
    /// </summary>
    /// <seealso cref="IAnimalManager" />
    public class AnimalManager : IAnimalManager
    {
        #region Properties and Constructor
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The animal DAL
        /// </summary>
        private readonly AnimalDal _animalDal;
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalManager"/> class.
        /// </summary>
        /// <param name="imapper">The imapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="getAnimalRepository">The get animal repository.</param>
        /// <param name="crudAnimalRepository">The crud animal repository.</param>
        public AnimalManager(IMapper imapper, ILogger<AnimalManager> logger, IGetData<Animal> getAnimalRepository, ICrudBasic<Animal> crudAnimalRepository)
        {
            _logger = logger;
            _animalDal = new AnimalDal(imapper, logger, getAnimalRepository, crudAnimalRepository);
        }
        #endregion

        #region GET                
        /// <summary>
        /// Gets all animals asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">guid, There was an error getting all the animals data.</exception>
        public async Task<ICollection<ZooAnimalDto>> GetAllAnimalsAsync()
        {
            try
            {
                return await _animalDal.GetAllAnimalsAsync();
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting all the animals data.");
            }
        }

        /// <summary>
        /// Gets all animals by type animal asynchronous.
        /// </summary>
        /// <param name="animalTypeId">The animal type identifier.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">There was an error getting all the animals data.</exception>
        public async Task<ICollection<ZooAnimalDto>> GetAllAnimalsByTypeAnimalAsync(short animalTypeId)
        {
            try
            {
                return await _animalDal.GetAllAnimalsByPredicateAsync(d => d.IdAnimalType == animalTypeId);
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting all the animals data.");
            }
        }

        /// <summary>
        /// Gets the animal by identifier asynchronous.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">There was an error getting all the animals data.</exception>
        public async Task<ZooAnimalDto> GetAnimalByIdAsync(int animalId)
        {
            try
            {
                return await _animalDal.GetAnimalsByPredicateAsync(d => d.Id == animalId);
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting the animal data.");
            }
        }
        #endregion

        #region POST                
        /// <summary>
        /// Creates the animal asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Status for the creating</returns>
        /// <exception cref="ExceptionDto">There was an error when storing the animal information.</exception>
        public async Task<int> CreateAnimalAsync(ZooAnimalRegisterDto obj, Guid userId)
        {
            try
            {
                var animalDto = await _animalDal.GetAnimalsByPredicateAsync(d => d.Code == obj.Code);
                if (animalDto != null)
                    throw new ExceptionDto(Guid.NewGuid(), "The animal code is already in use.");
                return await _animalDal.CreateAnimalAsync(obj, userId);
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error when storing the animal information.");
            }
        }
        #endregion

        #region PUT                        
        /// <summary>
        /// Updates the animal asynchronous.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        /// <param name="obj">The object.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Status for the updatig</returns>
        /// <exception cref="ExceptionDto">There was an error when storing the animal information.</exception>
        public async Task<int> UpdateAnimalAsync(int animalId, ZooAnimalRegisterDto obj, Guid userId)
        {
            try
            {
                var animalDto = await _animalDal.GetAnimalsByPredicateAsync(d => d.Code == obj.Code && d.Id != animalId);
                if (animalDto != null)
                    throw new ExceptionDto(Guid.NewGuid(), "The animal code is already in use.");
                return await _animalDal.UpdateAnimalAsync(animalId, obj, userId);
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error when update the animal information.");
            }
        }
        #endregion

        #region DELETE                        
        /// <summary>
        /// Deletes the animal asynchronous.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        /// <returns>Status for the deleting</returns>
        /// <exception cref="ExceptionDto">There was an error when storing the animal information.</exception>
        public async Task<int> DeleteAnimalAsync(int animalId)
        {
            try
            {
                var animalDto = await _animalDal.GetAnimalsByPredicateAsync(d => d.Id == animalId);
                if (animalDto == null)
                    throw new ExceptionDto(Guid.NewGuid(), "There is no animal to eliminate.");
                return await _animalDal.DeleteAnimalAsync(animalId);
            }
            catch (ExceptionDto)
            {
                throw;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error when delete the animal information.");
            }
        }
        #endregion
    }
}
