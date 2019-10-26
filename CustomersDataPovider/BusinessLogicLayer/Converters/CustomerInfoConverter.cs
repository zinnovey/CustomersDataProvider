using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessLogicLayer.DataTransferObjects;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Converters
{
    class CustomerInfoConverter
    {
        #region Public

        public static CustomerDTO ConvertCustomerInfo(CustomerEntity customerEntity, IEnumerable<TransactionEntity> transactionEntities) =>
            new CustomerDTO
            {
                CustomerID = customerEntity.Id,
                Name = customerEntity.Name,
                Email = customerEntity.ContactEmail,
                Mobile = customerEntity.MobileNumber.ToString(CultureInfo.InvariantCulture),
                Transactions = transactionEntities.Select(ConvertTransaction).ToList()
            };

        #endregion

        #region Private
        
        private static TransactionDTO ConvertTransaction(TransactionEntity transactionEntity) =>
            new TransactionDTO
            {
                Id = transactionEntity.CustomerId,
                Date = transactionEntity.DateTime,
                Amount = transactionEntity.Amount,
                Currency = transactionEntity.Currency.ToString(),
                Status = transactionEntity.Status.ToString()
            };

        #endregion

    }
}
