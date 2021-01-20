using System.Collections.Generic;
using System.Threading.Tasks;
using ZooDto;

namespace ZooService.Contracts.Manager
{
    /// <summary>
    /// Interface for management the types animals data
    /// </summary>
    public interface ITypeAnimalManager
    {
        /// <summary>
        /// Gets all type animals asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ZooTypeAnimalDto>> GetAllTypeAnimalsAsync();
    }
}
