using System;
using BusinessLogicLayer.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BusinessLogicLayer.Abstraction;

namespace BusinessLogicLayer.Validators
{
    public class QueryParametersValidator : IQueryParametersValidator
    {
        #region Constants

        private const Int32 MaxCustomerIdLength = 10;
        private const String EmailRegex = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        
        private const String NoCriteriaError = "No inquiry criteria!";
        private const String InvalidCustomerIdError = "Invalid Customer ID!";
        private const String InvalidCustomerEmailError = "Invalid Email!";

        #endregion

        #region Fields

        private Regex _emailRegex = new Regex(EmailRegex);

        #endregion

        #region IQueryParametersValidator

        public Boolean ValidateQueryParameters(CustomerInfoCriteriaDTO criteria, out ICollection<String> errors)
        {
            errors = new List<String>();

            if (String.IsNullOrEmpty(criteria.CustomerID) && String.IsNullOrEmpty(criteria.Email))
            {
                errors.Add(NoCriteriaError);
                return false;
            }

            if (!String.IsNullOrEmpty(criteria.CustomerID) && !IsCustomerIdValid(criteria.CustomerID))
                errors.Add(InvalidCustomerIdError);

            if (!String.IsNullOrEmpty(criteria.Email) && !IsCustomerEmailValid(criteria.Email))
                errors.Add(InvalidCustomerEmailError);

            return !errors.Any();
        }

        #endregion

        #region Private 

        private Boolean IsCustomerIdValid(String customerId)
        {
            if (customerId.Length > MaxCustomerIdLength)
                return false;

            if (!customerId.ToCharArray().All(Char.IsDigit))
                return false;

            return true;
        }

        private Boolean IsCustomerEmailValid(String customerEmail) 
            => _emailRegex.IsMatch(customerEmail);

        #endregion

    }
}
