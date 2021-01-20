using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZooDto;

namespace ZooService.Contracts.Manager
{
    /// <summary>
    /// Interface for management animal's information
    /// </summary>
    public interface IAnimalManager
    {
        /// <summary>
        /// Gets all animals asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ZooAnimalDto>> GetAllAnimalsAsync();
        /// <summary>
        /// Gets all animals by type animal asynchronous.
        /// </summary>
        /// <param name="animalTypeId">The animal type identifier.</param>
        /// <returns></returns>
        Task<ICollection<ZooAnimalDto>> GetAllAnimalsByTypeAnimalAsync(short animalTypeId);
        /// <summary>
        /// Creates the animal asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<int> CreateAnimalAsync(ZooAnimalRegisterDto obj, Guid userId);
        /// <summary>
        /// Gets the animal by identifier asynchronous.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        /// <returns></returns>
        Task<ZooAnimalDto> GetAnimalByIdAsync(int animalId);
        /// <summary>
        /// Updates the animal asynchronous.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        /// <param name="obj">The object.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<int> UpdateAnimalAsync(int animalId, ZooAnimalRegisterDto obj, Guid userId);
        /// <summary>
        /// Deletes the animal asynchronous.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        /// <returns></returns>
        Task<int> DeleteAnimalAsync(int animalId);
    }
}
