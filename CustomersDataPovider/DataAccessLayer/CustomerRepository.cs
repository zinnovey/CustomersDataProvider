using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public sealed class CustomerRepository : ICustomerRepository, IDisposable
    {
        #region Fields

        private readonly CustomersDBContext _dbContext;
        private readonly DbSet<CustomerEntity> _customersDbSet;

        #endregion

        #region Constructors

        public CustomerRepository(CustomersDBContext dbContext)
        {
            _dbContext = dbContext;
            _customersDbSet = _dbContext.Customers;
        }

        #endregion

        #region ICustomerRepository

        public IQueryable<CustomerEntity> Get(Expression<Func<CustomerEntity, Boolean>> filter = null)
        {
            IQueryable<CustomerEntity> query = _customersDbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

        public async Task<CustomerEntity> GetByIdAsync(Int32 id)
        {
            return await _customersDbSet.FindAsync(id)
                .ConfigureAwait(false);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        #endregion

    }
}
