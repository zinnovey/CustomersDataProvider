using BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Abstraction
{
    public interface IQueryParametersValidator
    {

        Boolean ValidateQueryParameters(CustomerInfoCriteriaDTO criteria, out ICollection<String> errors);

    }
}
