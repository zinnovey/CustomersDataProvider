using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Validators
{
    public class QueryParametersValidator
    {
        #region Constants

        private const String EmailRegex = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        private const Int32 MaxCustomerIdLength = 10;

        private const String CustomerIdLengthError = "";
        private const String CustomerIdCharactersError = "";

        private const String CustomerEmailError = "";

        #endregion

        #region Fields

        private Regex _emailRegex = new Regex(EmailRegex);

        #endregion

        #region Public 

        public Boolean ValidateCustomerId(String customerId, out ICollection<String> errors)
        {
            var validationErrors = new List<String>();

            if (customerId.Length > MaxCustomerIdLength)
                validationErrors.Add(CustomerIdLengthError);

            if (!customerId.ToCharArray().All(Char.IsDigit))
                validationErrors.Add(CustomerIdCharactersError);

            errors = validationErrors;
            return !errors.Any();
        }

        public Boolean ValidateCustomerEmail(String customerEmail, out ICollection<String> errors)
        {
            errors = new List<String>();

            if (!_emailRegex.IsMatch(customerEmail))
                errors.Add(CustomerEmailError);

            return !errors.Any();
        }

        public Boolean ValidateQueryParameters(String customerId, String customerEmail, out ICollection<String> errors)
        {
            errors = new List<String>();

            if (!ValidateCustomerId(customerId, out ICollection<String> customerIdErrors))
                errors.ToList().AddRange(customerIdErrors);

            if (!ValidateCustomerEmail(customerEmail, out ICollection<String> customerEmailErrors))
                errors.ToList().AddRange(customerEmailErrors);

            return !errors.Any();

        }

        #endregion

    }
}
