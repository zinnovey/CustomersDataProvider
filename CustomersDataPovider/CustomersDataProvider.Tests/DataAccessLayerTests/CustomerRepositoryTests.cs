using System;
using System.Linq;
using System.Threading.Tasks;
using CustomersDataProvider.DataAccessLayer;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.DataAccessLayerTests
{
    [TestFixture]
    class CustomerRepositoryTests
    {
        #region Fields

        private ICustomerRepository _customerRepository;

        #endregion

        #region SetUp

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _customerRepository = new CustomerRepository(new CustomersDBContext());
        }

        #endregion

        #region GetByIdTests

        [Test]
        public async Task GetByIdTest1()
        {
            var customer = await _customerRepository.GetByIdAsync(1)
                .ConfigureAwait(false);

            Assert.NotNull(customer);

            Assert.AreEqual("Bob", customer.Name);
            Assert.AreEqual("bob@gmail.com", customer.ContactEmail);
            Assert.AreEqual(38096074410, customer.MobileNumber);
        }

        [Test]
        public async Task GetByIdTest2()
        {
            var customer = await _customerRepository.GetByIdAsync(45)
                .ConfigureAwait(false);

            Assert.AreEqual(null, customer);
        }

        #endregion

        #region GetTests

        [Test]
        public async Task GetTest1()
        {
            var customer = await _customerRepository.Get(x => x.Id == 1 && x.ContactEmail == "bob@gmail.com")
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            Assert.NotNull(customer);

            Assert.AreEqual("Bob", customer.Name);
            Assert.AreEqual("bob@gmail.com", customer.ContactEmail);
            Assert.AreEqual(38096074410, customer.MobileNumber);
        }

        [Test]
        public async Task GetTest2()
        {
            var customer = await _customerRepository.Get(x => x.Id == 48 && x.ContactEmail == "bob@gmail.com")
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            Assert.AreEqual(null, customer);
        }

        [Test]
        public async Task GetTest3()
        {
            var customer = await _customerRepository.Get(x => x.Id == 1)
                .Include(x => x.Transactions)
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            Assert.NotNull(customer);
            Assert.NotNull(customer.Transactions);

            var transaction = customer.Transactions.First();
            Assert.NotNull(transaction);

            Assert.AreEqual(1, transaction.CustomerId);
            Assert.AreEqual(new DateTime(2007,5, 8, 12,35,0), transaction.DateTime);
            Assert.AreEqual(151345.54, transaction.Amount);
            Assert.AreEqual(Currency.EUR, transaction.Currency);
            Assert.AreEqual(TransactionStatus.Success, transaction.Status);
        }

        [Test]
        public async Task GetTest4()
        {
            var customer = await _customerRepository.Get(x => x.Id == 1)
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            Assert.NotNull(customer);
            Assert.AreEqual(0, customer.Transactions.Count);
        }

        #endregion

    }
}
