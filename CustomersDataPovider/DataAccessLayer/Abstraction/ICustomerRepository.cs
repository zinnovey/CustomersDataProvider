using DataAccessLayer.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstraction
{
    public interface ICustomerRepository
    {

        IQueryable<CustomerEntity> Get(Expression<Func<CustomerEntity, Boolean>> filter = null);

        Task<CustomerEntity> GetByIdAsync(Int32 id);

    }
}
