using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Converters;
using BusinessLogicLayer.DataTransferObjects;
using DataAccessLayer;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CustomerInfoServiceProvider : ICustomerInfoServiceProvider
    {
        private ICustomerRepository _customerRepository;

        public CustomerInfoServiceProvider(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria)
        {
            CustomerEntity customerEntity;

            if (!String.IsNullOrEmpty(criteria.CustomerID))
            {
                customerEntity = await _customerRepository.Get(x => x.Id == Int32.Parse(criteria.CustomerID))
                    .Include(x => x.Transactions)
                    .AsNoTracking<CustomerEntity>()
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);

                if (!String.IsNullOrEmpty(criteria.Email) && customerEntity?.ContactEmail != criteria.Email)
                    return null;
            }
            else
                customerEntity = await _customerRepository.Get(x => x.ContactEmail == criteria.Email)
                    .Include(x => x.Transactions)
                    .AsNoTracking<CustomerEntity>()
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);

            if (customerEntity is null) return null;

            return CustomerInfoConverter.ConvertCustomerInfo(customerEntity);
        }
    }
}
