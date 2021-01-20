using System.Threading.Tasks;

namespace ZooService.Contracts.Repository
{
    /// <summary>
    /// Méthods for Create, Update and Delete
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudBasic<T> where T : class
    {
        /// <summary>
        /// Adds the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        void Add(T obj);
        /// <summary>
        /// Updates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        void Update(T obj);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
