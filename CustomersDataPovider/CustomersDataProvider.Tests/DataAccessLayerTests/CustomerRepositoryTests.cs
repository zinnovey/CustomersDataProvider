using System.Threading.Tasks;
using CustomersDataProvider.DataAccessLayer;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities;
using CustomersDataProvider.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.DataAccessLayerTests
{
    [TestFixture]
    class CustomerRepositoryTests
    {
        #region Fields

        private IRepository<CustomerEntity> _customerRepository;

        #endregion

        #region SetUp

        [SetUp]
        public void SetUp() 
            => _customerRepository = new GenericRepository<CustomerEntity>(new CustomersDBContext(null));

        #endregion

        #region GetByIdTests

        [Test]
        public async Task GetByIdTest1()
        {
            var customer = await _customerRepository.GetByIdAsync(1)
                .ConfigureAwait(false);

            AssertFirstCustomer(customer);
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

            AssertFirstCustomer(customer);
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

        #endregion

        #region Private 

        private void AssertFirstCustomer(CustomerEntity customer)
        {
            Assert.NotNull(customer);

            Assert.AreEqual("Bob", customer.Name);
            Assert.AreEqual("bob@gmail.com", customer.ContactEmail);
            Assert.AreEqual(38096074410, customer.MobileNumber);
        }

        #endregion

    }
}
