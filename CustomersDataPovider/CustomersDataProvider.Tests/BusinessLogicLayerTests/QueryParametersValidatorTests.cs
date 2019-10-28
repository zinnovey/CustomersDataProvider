using System;
using System.Collections.Generic;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using CustomersDataProvider.BusinessLogicLayer.Validators;
using NUnit.Framework;

namespace CustomersDataProvider.Tests.BusinessLogicLayerTests
{
    [TestFixture]
    class QueryParametersValidatorTests
    {
        #region Fields

        private IQueryParametersValidator _queryParametersValidator;

        #endregion

        #region SetUp

        [SetUp]
        public void SetUp() 
            => _queryParametersValidator = new QueryParametersValidator();

        #endregion

        #region ValidateCustomerIdTests

        [Test]
        public void ValidateCustomerIdTest1()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "123456"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, true);
            Assert.IsEmpty(errors);
        }

        [Test]
        public void ValidateCustomerIdTest2()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "as"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, false);
            Assert.IsNotEmpty(errors);
        }

        [Test]
        public void ValidateCustomerIdTest3()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "12345678900"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, false);
            Assert.IsNotEmpty(errors);
        }

        #endregion

        #region ValidateCustomerEmailTests
        
        [Test]
        public void ValidateCustomerEmailTest1()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                Email = "myemail@domain.com"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, true);
            Assert.IsEmpty(errors);
        }
        [Test]
        public void ValidateCustomerEmailTest2()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                Email = "my-email"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, false);
            Assert.IsNotEmpty(errors);
        }

        #endregion

        #region ValidateQueryParametersTests

        [Test]
        public void ValidateQueryParametersTest1()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "123456",
                Email = "my-email@domain.com"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, true);
            Assert.IsEmpty(errors);
        }

        [Test]
        public void ValidateQueryParametersTest2()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "123456",
                Email = "my-email"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, false);
            Assert.IsNotEmpty(errors);
        }

        [Test]
        public void ValidateQueryParametersTest3()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "123456a",
                Email = "my-email@domain.com"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, false);
            Assert.IsNotEmpty(errors);
        }

        [Test]
        public void ValidateQueryParametersTest4()
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = "123456a",
                Email = "my-email"
            };

            var isValid = _queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors);

            Assert.AreEqual(isValid, false);
            Assert.IsNotEmpty(errors);
        }

        #endregion
        
    }
}
