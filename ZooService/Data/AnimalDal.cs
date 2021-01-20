using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZooDto;
using ZooService.Contracts.Repository;
using ZooService.Models;

namespace ZooService.Data
{
    /// <summary>
    /// Data Access Layer for Animal
    /// </summary>
    public class AnimalDal
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
        /// The get animal repository
        /// </summary>
        private readonly IGetData<Animal> _getAnimalRepository;
        /// <summary>
        /// The crud animal repository
        /// </summary>
        private readonly ICrudBasic<Animal> _crudAnimalRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalDal"/> class.
        /// </summary>
        /// <param name="imapper">The imapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="getAnimalRepository">The get animal repository.</param>
        /// <param name="crudAnimalRepository">The crud animal repository.</param>
        public AnimalDal(IMapper imapper, ILogger logger, IGetData<Animal> getAnimalRepository, ICrudBasic<Animal> crudAnimalRepository)
        {
            _imapper = imapper;
            _logger = logger;
            _getAnimalRepository = getAnimalRepository;
            _crudAnimalRepository = crudAnimalRepository;
        }
        #endregion

        #region GET        
        /// <summary>
        /// Gets all animals asynchronous.
        /// </summary>
        /// <returns>Return a list of animal's information</returns>
        /// <exception cref="ExceptionDto">There was an error getting all the employee charge data.</exception>
        public async Task<ICollection<ZooAnimalDto>> GetAllAnimalsAsync()
        {
            try
            {
                ICollection<ZooAnimalDto> lstAnimaslDto = new List<ZooAnimalDto>();
                var query = _getAnimalRepository.Get().Include(a => a.AnimalType);
                var lstAnimals = await query.ToListAsync();
                lstAnimaslDto = _imapper.Map<ICollection<Animal>, ICollection<ZooAnimalDto>>(lstAnimals);
                return lstAnimaslDto;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting all the animals data.");
            }
        }

        /// <summary>
        /// Gets all animals by predicate asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">There was an error getting all the animals data.</exception>
        public async Task<ICollection<ZooAnimalDto>> GetAllAnimalsByPredicateAsync(Expression<Func<Animal, bool>> predicate)
        {
            try
            {
                ICollection<ZooAnimalDto> lstAnimalsDto = new List<ZooAnimalDto>();
                var query = _getAnimalRepository.Get().Include(a => a.AnimalType).Where(predicate);
                var lstAnimals = await query.ToListAsync();
                lstAnimalsDto = _imapper.Map<ICollection<Animal>, ICollection<ZooAnimalDto>>(lstAnimals);
                return lstAnimalsDto;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting all the animals data.");
            }
        }

        /// <summary>
        /// Gets all animals by predicate asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionDto">There was an error getting all the animals data.</exception>
        public async Task<ZooAnimalDto> GetAnimalsByPredicateAsync(Expression<Func<Animal, bool>> predicate)
        {
            try
            {
                var animalDto = new ZooAnimalDto();
                var animal = await _getAnimalRepository.Get().Include(a => a.AnimalType).FirstOrDefaultAsync(predicate);
                animalDto = _imapper.Map<Animal, ZooAnimalDto>(animal);
                return animalDto;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionDto(guid, "There was an error getting one animal data.");
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

                var animal = _imapper.Map<ZooAnimalRegisterDto, Animal>(obj);
                animal.DateCreated = DateTime.Now;
                animal.UserCreated = userId;
                _crudAnimalRepository.Add(animal);
                await _crudAnimalRepository.SaveChangesAsync();
                return animal.Id;
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
        /// <returns>Status for the updating</returns>
        /// <exception cref="ExceptionDto">There was an error when update the animal information.</exception>
        public async Task<int> UpdateAnimalAsync(int animalId, ZooAnimalRegisterDto obj, Guid userId)
        {
            try
            {
                var animal = await _getAnimalRepository.Get().FirstOrDefaultAsync(d => d.Id == animalId);
                _imapper.Map(obj, animal);
                animal.DateUpdated = DateTime.Now;
                animal.UserUpdated = userId;
                _crudAnimalRepository.Update(animal);
                await _crudAnimalRepository.SaveChangesAsync();
                return animal.Id;
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
        /// <exception cref="ExceptionDto">There was an error when delete the animal information.</exception>
        public async Task<int> DeleteAnimalAsync(int animalId)
        {
            try
            {
                var animal = await _getAnimalRepository.Get().FirstOrDefaultAsync(d => d.Id == animalId);
                _crudAnimalRepository.Delete(animal);
                await _crudAnimalRepository.SaveChangesAsync();
                return animal.Id;
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
