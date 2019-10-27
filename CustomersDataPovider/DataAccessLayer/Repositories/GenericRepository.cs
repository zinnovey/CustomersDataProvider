using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomersDataProvider.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region Fields

        private readonly CustomersDBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        #endregion

        #region Constructors

        public GenericRepository(CustomersDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #endregion

        #region IRepository

        public virtual IQueryable<T> Get(Expression<Func<T, Boolean>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public virtual async Task<T> GetByIdAsync(Int32 id)
        {
            return await _dbSet.FindAsync(id)
                .ConfigureAwait(false);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        #endregion

    }
}
