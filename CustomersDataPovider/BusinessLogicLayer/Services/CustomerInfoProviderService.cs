using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.Converters;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomersDataProvider.BusinessLogicLayer.Services
{
    public class CustomerInfoProviderService : ICustomerInfoProviderService
    {
        #region Constants

        private const Int32 MaxCustomerTransactionsCount = 5;

        #endregion

        #region Fields

        private readonly IRepository<CustomerEntity> _customerRepository;
        private readonly IRepository<TransactionEntity> _transactionRepository;

        #endregion

        #region Constructors

        public CustomerInfoProviderService(IRepository<CustomerEntity> customerRepository,
            IRepository<TransactionEntity> transactionRepository)
        {
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
        }

        #endregion

        #region ICustomerInfoServiceProvider

        public async Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria)
        {
            var customerEntity = await _customerRepository
                .Get(x => (String.IsNullOrEmpty(criteria.CustomerID) || x.Id == Int32.Parse(criteria.CustomerID)) && 
                          (String.IsNullOrEmpty(criteria.Email) || x.ContactEmail == criteria.Email))
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            if (customerEntity is null) return null;

            customerEntity.Transactions = await GetTransactions(customerEntity.Id)
                .ConfigureAwait(false);
            
            return CustomerInfoConverter.ConvertCustomerInfo(customerEntity);
        }

        #endregion

        #region Private

        private async Task<ICollection<TransactionEntity>> GetTransactions(Int32 customerId)
        {
            var transactions = await _transactionRepository
                .Get(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.DateTime)
                .Take(MaxCustomerTransactionsCount)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return transactions.ToHashSet();
        }

        #endregion

    }
}
