using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomersDataProvider.DataAccessLayer.Abstraction
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> Get(Expression<Func<T, Boolean>> filter = null);

        Task<T> GetByIdAsync(Int32 id);
    }
}
