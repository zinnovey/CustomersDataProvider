﻿using System;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Converters;
using BusinessLogicLayer.DataTransferObjects;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CustomerInfoServiceProvider : ICustomerInfoServiceProvider
    {
        #region Fields

        private readonly ICustomerRepository _customerRepository;

        #endregion

        #region Constructors

        public CustomerInfoServiceProvider(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #endregion

        #region ICustomerInfoServiceProvider

        public async Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria)
        {
            CustomerEntity customerEntity;

            //TODO: add logic for getting latest 5 transactions
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
