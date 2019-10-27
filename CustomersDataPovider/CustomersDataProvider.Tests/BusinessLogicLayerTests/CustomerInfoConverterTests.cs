using System;
using System.Collections.Generic;
using System.Linq;
using CustomersDataProvider.BusinessLogicLayer.Converters;
using CustomersDataProvider.DataAccessLayer.Entities;
using CustomersDataProvider.DataAccessLayer.Entities.Enums;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    class CustomerInfoConverterTests
    {
        #region Tests

        [Test]
        public void ConvertCustomerEntityTest()
        {
            var customerEntity = new CustomerEntity
            {
                Id = 1,
                Name = "Bob",
                ContactEmail = "bob@gmail.com",
                MobileNumber = 38096074410
            };

            // ReSharper disable once InconsistentNaming
            var customerInfoDTO = CustomerInfoConverter.ConvertCustomerInfo(customerEntity);

            Assert.NotNull(customerInfoDTO);

            Assert.AreEqual(customerEntity.Id, customerInfoDTO.CustomerID);
            Assert.AreEqual(customerEntity.Name, customerInfoDTO.Name);
            Assert.AreEqual(customerEntity.ContactEmail, customerInfoDTO.Email);
            Assert.AreEqual(customerEntity.MobileNumber, Decimal.Parse(customerInfoDTO.Mobile));

        }

        [Test]
        public void ConvertCustomerEntityWithTransactionsTest()
        {
            var customerEntity = new CustomerEntity
            {
                Id = 2,
                Name = "Tom",
                ContactEmail = "tom@gmail.com",
                MobileNumber = 380965436771,
                Transactions = new List<TransactionEntity>
                {
                    new TransactionEntity
                    {
                        Id = 7,
                        CustomerId = 2,
                        DateTime = new DateTime(1968, 10, 23, 12, 46, 0),
                        Amount = 12.43m,
                        Currency = Currency.GBP,
                        Status = TransactionStatus.Canceled
                    },
                    new TransactionEntity
                    {
                        Id = 8,
                        CustomerId = 2,
                        DateTime = new DateTime(1968, 10, 23, 12, 46, 0),
                        Amount = 2342.56m,
                        Currency = Currency.GBP,
                        Status = TransactionStatus.Success
                    }
                }
            };

            // ReSharper disable once InconsistentNaming
            var customerInfoDTO = CustomerInfoConverter.ConvertCustomerInfo(customerEntity);

            Assert.NotNull(customerInfoDTO);
            Assert.NotNull(customerInfoDTO.Transactions);

            Assert.AreEqual(customerEntity.Transactions.Count, customerInfoDTO.Transactions.Count);

            var transactionEntity = customerEntity.Transactions.First();
            // ReSharper disable once InconsistentNaming
            var transactionDTO = customerInfoDTO.Transactions.First();

            Assert.AreEqual(transactionEntity.Id, transactionDTO.Id);
            Assert.AreEqual(transactionEntity.DateTime, transactionDTO.Date);
            Assert.AreEqual(transactionEntity.Amount, transactionDTO.Amount);
            Assert.AreEqual(transactionEntity.Currency.ToString(), transactionDTO.Currency);
            Assert.AreEqual(transactionEntity.Status.ToString(), transactionDTO.Status);
        }

        #endregion

    }
}
