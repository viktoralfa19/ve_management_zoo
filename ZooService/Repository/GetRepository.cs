using Microsoft.EntityFrameworkCore;
using ZooService.Contracts.Repository;

namespace ZooService.Repository
{
    /// <summary>
    /// Get data ffrom Database
    /// </summary>
    /// <seealso cref="ZooService.Contracts.Repository.IGetData{T}" />
    public class GetRepository<T> : IGetData<T> where T : class
    {
        #region Properties and Constructor
        /// <summary>
        /// The database context
        /// </summary>
        internal ZooContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRepository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetRepository(ZooContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public System.Linq.IQueryable<T> Get()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }
        #endregion
    }
}
