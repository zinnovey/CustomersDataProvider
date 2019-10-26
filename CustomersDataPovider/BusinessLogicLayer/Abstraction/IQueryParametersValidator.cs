using BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Abstraction
{
    public interface IQueryParametersValidator
    {

        Boolean ValidateQueryParameters(CustomerInfoCriteriaDTO criteria, out ICollection<String> errors);

    }
}
