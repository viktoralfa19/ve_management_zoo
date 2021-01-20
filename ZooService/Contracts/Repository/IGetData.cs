using System.Linq;

namespace ZooService.Contracts.Repository
{
    /// <summary>
    /// Get data from repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGetData<T> where T: class
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get();
    }
}
