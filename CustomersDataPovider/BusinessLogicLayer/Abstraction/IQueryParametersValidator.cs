using System;
using System.Collections.Generic;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;

namespace CustomersDataProvider.BusinessLogicLayer.Abstraction
{
    public interface IQueryParametersValidator
    {

        Boolean ValidateQueryParameters(CustomerInfoCriteriaDTO criteria, out ICollection<String> errors);

    }
}
