using System;
using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.Converters;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomersDataProvider.BusinessLogicLayer.Services
{
    public class CustomerInfoServiceProvider : ICustomerInfoServiceProvider
    {
        #region Fields

        private readonly IRepository<CustomerEntity> _customerRepository;
        private readonly IRepository<TransactionEntity> _transactionRepository;

        #endregion

        #region Constructors

        public CustomerInfoServiceProvider(IRepository<CustomerEntity> customerRepository,
            IRepository<TransactionEntity> transactionRepository)
        {
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
        }

        #endregion

        #region ICustomerInfoServiceProvider

        public async Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria)
        {
            CustomerEntity customerEntity;

            //TODO: add logic for getting the latest 5 transactions
            if (!String.IsNullOrEmpty(criteria.CustomerID))
            {
                customerEntity = await _customerRepository.Get(x => x.Id == Int32.Parse(criteria.CustomerID))
                    .Include(x => x.Transactions)
                    .AsNoTracking()
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);

                if (!String.IsNullOrEmpty(criteria.Email) && customerEntity?.ContactEmail != criteria.Email)
                    return null;
            }
            else
                customerEntity = await _customerRepository.Get(x => x.ContactEmail == criteria.Email)
                    .Include(x => x.Transactions)
                    .AsNoTracking()
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);

            return customerEntity is null
                ? null 
                : CustomerInfoConverter.ConvertCustomerInfo(customerEntity);
        }

        #endregion

    }
}
