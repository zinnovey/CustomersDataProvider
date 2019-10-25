using System;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public sealed class CustomerRepository : IDisposable
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

        #region Public

        public IQueryable<CustomerEntity> Get(Expression<Func<CustomerEntity, Boolean>> filter = null)
        {
            IQueryable<CustomerEntity> query = _customersDbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

        public CustomerEntity GetById(Int32 id)
        {
            return _customersDbSet.Find(id);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        #endregion

    }
}
