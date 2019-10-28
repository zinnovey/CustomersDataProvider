using System;
using System.Threading.Tasks;
using CustomersDataProvider.DataAccessLayer;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Entities;
using CustomersDataProvider.DataAccessLayer.Entities.Enums;
using CustomersDataProvider.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.DataAccessLayerTests
{
    [TestFixture]
    class TransactionRepositoryTests
    {
        #region Fields

        private IRepository<TransactionEntity> _transactionRepository;

        #endregion

        #region SetUp

        [OneTimeSetUp]
        public void SetUp()
            => _transactionRepository = new GenericRepository<TransactionEntity>(new CustomersDBContext(null));

        #endregion

        #region GetByIdTests

        [Test]
        public async Task GetByIdTest1()
        {
            var transaction = await _transactionRepository.GetByIdAsync(1)
                .ConfigureAwait(false);

            AssertFirstTransaction(transaction);
        }

        [Test]
        public async Task GetByIdTest2()
        {
            var transaction = await _transactionRepository.GetByIdAsync(45)
                .ConfigureAwait(false);

            Assert.AreEqual(null, transaction);
        }

        #endregion

        #region GetTests

        [Test]
        public async Task GetTest1()
        {
            var transaction = await _transactionRepository.Get(x => x.CustomerId == 1)
                .AsNoTracking()
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            AssertFirstTransaction(transaction);
        }

        [Test]
        public async Task GetTest2()
        {
            var transaction = await _transactionRepository.Get(x => x.CustomerId == 45)
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            Assert.AreEqual(null, transaction);
        }

        #endregion

        #region Private

        private void AssertFirstTransaction(TransactionEntity transaction)
        {
            Assert.NotNull(transaction);

            Assert.AreEqual(1, transaction.Id);
            Assert.AreEqual(1, transaction.CustomerId);
            Assert.AreEqual(new DateTime(2007, 5, 8, 12, 34, 0), transaction.DateTime);
            Assert.AreEqual(151345.54m, transaction.Amount);
            Assert.AreEqual(Currency.EUR, transaction.Currency);
            Assert.AreEqual(TransactionStatus.Success, transaction.Status);
        }

        #endregion

    }
}
