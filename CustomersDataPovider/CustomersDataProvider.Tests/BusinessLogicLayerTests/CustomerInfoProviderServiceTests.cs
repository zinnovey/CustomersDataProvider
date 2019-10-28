using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using CustomersDataProvider.BusinessLogicLayer.Services;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities;
using CustomersDataProvider.DataAccessLayer.Entities.Enums;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    class CustomerInfoProviderServiceTests
    {
        #region Fields

        private ICustomerInfoProviderService _customerInfoServiceProvider;
        private Mock<IRepository<CustomerEntity>> _customerRepositoryMock;
        private Mock<IRepository<TransactionEntity>> _transactionRepositoryMock;

        #endregion

        #region SetUp

        [SetUp]
        public void SetUp()
        {
            _customerRepositoryMock = new Mock<IRepository<CustomerEntity>>();
            _transactionRepositoryMock = new Mock<IRepository<TransactionEntity>>();
        }
            

        #endregion

        #region GetCustomerInfoByIdTests

        [Test]
        public async Task GetCustomerInfoByIdTest1()
        {
            SetUpMocksForFirstCustomer();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForFirstCustomer();

            AssertFirstCustomer(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByIdTest2()
        {
            SetUpMocksForEmptyResult();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1234"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForEmptyResult();

            AssertEmptyResult(customerDTO);
        }

        #endregion

        #region GetCustomerInfoByIdTests

        [Test]
        public async Task GetCustomerInfoByEmailTest1()
        {
            SetUpMocksForFirstCustomer();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                Email = "bob@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForFirstCustomer();

            AssertFirstCustomer(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByEmailTest2()
        {
            SetUpMocksForEmptyResult();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                Email = "bobo@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForEmptyResult();

            AssertEmptyResult(customerDTO);
        }

        #endregion

        #region GetCustomerInfoByBothCriteriaTests

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest1()
        {
            SetUpMocksForFirstCustomer();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1",
                Email = "bob@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForFirstCustomer();

            AssertFirstCustomer(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest2()
        {
            SetUpMocksForEmptyResult();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1",
                Email = "bobo@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForEmptyResult();

            AssertEmptyResult(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest3()
        {
            SetUpMocksForEmptyResult();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1234",
                Email = "bob@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForEmptyResult();

            AssertEmptyResult(customerDTO);
        }

        [Test]
        public async Task GetCustomerInfoByBothCriteriaTest4()
        {
            SetUpMocksForEmptyResult();
            SetUpProviderService();

            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "1234",
                Email = "bobo@gmail.com"
            };

            // ReSharper disable once InconsistentNaming
            var customerDTO = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            VerifyMocksForEmptyResult();

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

        #region Private

        private void SetUpMocksForFirstCustomer()
        {
            var customersList = new List<CustomerEntity>
            {
                new CustomerEntity
                {
                    Id = 1,
                    Name = "Bob",
                    ContactEmail = "bob@gmail.com",
                    MobileNumber = 38096074410
                }
            };

            var transactionsList = new List<TransactionEntity>
            {
                new TransactionEntity
                {
                    Id = 1,
                    CustomerId = 1,
                    DateTime = new DateTime(2007, 5, 8, 12, 34, 0),
                    Amount = 151345.54m,
                    Currency = Currency.EUR,
                    Status = TransactionStatus.Success
                },
                new TransactionEntity
                {
                    Id = 2,
                    CustomerId = 1,
                    DateTime = new DateTime(2007, 5, 8, 12, 35, 0),
                    Amount = 123.22m,
                    Currency = Currency.EUR,
                    Status = TransactionStatus.Success
                },
                new TransactionEntity
                {
                    Id = 3,
                    CustomerId = 1,
                    DateTime = new DateTime(2007, 5, 8, 12, 35, 0),
                    Amount = 34.50m,
                    Currency = Currency.EUR,
                    Status = TransactionStatus.Failed
                },
                new TransactionEntity
                {
                    Id = 4,
                    CustomerId = 1,
                    DateTime = new DateTime(2007, 5, 8, 12, 35, 0),
                    Amount = 347.32m,
                    Currency = Currency.EUR,
                    Status = TransactionStatus.Canceled
                },
                new TransactionEntity
                {
                    Id = 5,
                    CustomerId = 1,
                    DateTime = new DateTime(2007, 5, 8, 12, 35, 0),
                    Amount = 121312.25m,
                    Currency = Currency.EUR,
                    Status = TransactionStatus.Success
                },
                new TransactionEntity
                {
                    Id = 6,
                    CustomerId = 1,
                    DateTime = new DateTime(2007, 5, 8, 12, 36, 0),
                    Amount = 4634.43m,
                    Currency = Currency.EUR,
                    Status = TransactionStatus.Success
                }
            };

            _customerRepositoryMock
                .Setup(x => x.Get(It.IsAny<Expression<Func<CustomerEntity, Boolean>>>()))
                .Returns(customersList.AsQueryable().BuildMock().Object);

            _transactionRepositoryMock
                .Setup(x => x.Get(It.IsAny<Expression<Func<TransactionEntity, Boolean>>>()))
                .Returns(transactionsList.AsQueryable().BuildMock().Object);
        }

        private void SetUpMocksForEmptyResult()
        {
            _customerRepositoryMock
                .Setup(x => x.Get(It.IsAny<Expression<Func<CustomerEntity, Boolean>>>()))
                .Returns(new List<CustomerEntity>().AsQueryable().BuildMock().Object);

            _transactionRepositoryMock
                .Setup(x => x.Get(It.IsAny<Expression<Func<TransactionEntity, Boolean>>>()))
                .Returns(new List<TransactionEntity>().AsQueryable().BuildMock().Object);
        }

        private void SetUpProviderService()
        {
            _customerInfoServiceProvider 
                = new CustomerInfoProviderService(_customerRepositoryMock.Object,
                    _transactionRepositoryMock.Object);
        }

        private void VerifyMocksForFirstCustomer()
        {
            _customerRepositoryMock.Verify(
                x => x.Get(It.IsAny<Expression<Func<CustomerEntity, Boolean>>>()), 
                Times.Once);

            _transactionRepositoryMock.Verify(
                x => x.Get(It.IsAny<Expression<Func<TransactionEntity, Boolean>>>()),
                Times.Once);
        }

        private void VerifyMocksForEmptyResult()
        {
            _customerRepositoryMock.Verify(
                x => x.Get(It.IsAny<Expression<Func<CustomerEntity, Boolean>>>()),
                Times.Once);

            _transactionRepositoryMock.Verify(
                x => x.Get(It.IsAny<Expression<Func<TransactionEntity, Boolean>>>()),
                Times.Never);
        }

        #endregion
    }
}
