using System.Globalization;
using System.Linq;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using CustomersDataProvider.DataAccessLayer.Entities;

namespace CustomersDataProvider.BusinessLogicLayer.Converters
{
    public class CustomerInfoConverter
    {
        #region Public

        public static CustomerDTO ConvertCustomerInfo(CustomerEntity customerEntity) =>
            new CustomerDTO
            {
                CustomerID = customerEntity.Id,
                Name = customerEntity.Name,
                Email = customerEntity.ContactEmail,
                Mobile = customerEntity.MobileNumber.ToString(CultureInfo.InvariantCulture),
                Transactions = customerEntity.Transactions.Select(ConvertTransaction).ToList()
            };

        #endregion

        #region Private
        
        private static TransactionDTO ConvertTransaction(TransactionEntity transactionEntity) =>
            new TransactionDTO
            {
                Id = transactionEntity.Id,
                Date = transactionEntity.DateTime,
                Amount = transactionEntity.Amount,
                Currency = transactionEntity.Currency.ToString(),
                Status = transactionEntity.Status.ToString()
            };

        #endregion

    }
}
