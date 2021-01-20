using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZooService.Contracts.Repository;

namespace ZooService.Repository
{
    /// <summary>
    /// Méthods for Create, Update and Delete
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ZooService.Contracts.Repository.ICrudBasic{T}" />
    public class CrudRepository<T> : ICrudBasic<T> where T : class
    {
        #region Properties and Constructor
        /// <summary>
        /// The database context
        /// </summary>
        internal ZooContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudRepository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public CrudRepository(ZooContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Add(T obj)
        {
            _dbContext.Set<T>().Add(obj);
        }

        /// <summary>
        /// Updates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Update(T obj)
        {
            if (_dbContext.Entry(obj).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(obj);
            }
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
                _dbContext.Set<T>().Remove(entity);
            }
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
