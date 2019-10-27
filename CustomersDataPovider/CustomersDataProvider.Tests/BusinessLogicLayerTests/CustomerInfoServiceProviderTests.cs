using System;
using System.Linq;
using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using CustomersDataProvider.BusinessLogicLayer.Services;
using CustomersDataProvider.DataAccessLayer;
using CustomersDataProvider.DataAccessLayer.Entities;
using CustomersDataProvider.DataAccessLayer.Entities.Enums;
using CustomersDataProvider.DataAccessLayer.Repositories;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    class CustomerInfoServiceProviderTests
    {
        #region Fields

        private ICustomerInfoProviderService _customerInfoServiceProvider;

        #endregion

        #region SetUp

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbContext = new CustomersDBContext();
            _customerInfoServiceProvider
                = new CustomerInfoProviderService(
                    new GenericRepository<CustomerEntity>(dbContext),
                    new GenericRepository<TransactionEntity>(dbContext));
        }
            

        #endregion

        #region GetCustomerInfoByIdTests

        [Test]
        public async Task GetCustomerInfoByIdTest1()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertFirstCustomer(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByIdTest2()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1234"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertEmptyResult(customerDTO);
        }

        #endregion

        #region GetCustomerInfoByIdTests

        [Test]
        public async Task GetCustomerInfoByEmailTest1()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                Email = "bob@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertFirstCustomer(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByEmailTest2()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                Email = "bobo@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertEmptyResult(customerDTO);
        }

        #endregion

        #region GetCustomerInfoByBothCriteriaTests

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest1()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1",
                Email = "bob@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertFirstCustomer(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest2()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1",
                Email = "bobo@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertEmptyResult(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest3()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1234",
                Email = "bob@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertEmptyResult(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest4()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1234",
                Email = "bobo@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            AssertEmptyResult(customerDTO);
        }

        #endregion

        #region Private

        // ReSharper disable once InconsistentNaming
        private void AssertFirstCustomer(CustomerDTO customerDTO)
        {
            Assert.NotNull(customerDTO);

            Assert.AreEqual(1, customerDTO.CustomerID);
            Assert.AreEqual("Bob", customerDTO.Name);
            Assert.AreEqual("bob@gmail.com", customerDTO.Email);
            Assert.AreEqual("38096074410", customerDTO.Mobile);

            Assert.NotNull(customerDTO.Transactions);
            Assert.IsNotEmpty(customerDTO.Transactions);

            // ReSharper disable once InconsistentNaming
            var transactionDTO = customerDTO.Transactions.First();

            Assert.AreEqual(6, transactionDTO.Id);
            Assert.AreEqual(new DateTime(2007, 5, 8, 12, 36, 0), transactionDTO.Date);
            Assert.AreEqual(4634.43m, transactionDTO.Amount);
            Assert.AreEqual(Currency.EUR.ToString(), transactionDTO.Currency);
            Assert.AreEqual(TransactionStatus.Success.ToString(), transactionDTO.Status);
        }
        
        // ReSharper disable once InconsistentNaming
        private void AssertEmptyResult(CustomerDTO customerDTO) 
            => Assert.AreEqual(null, customerDTO);

        #endregion
    }
}
