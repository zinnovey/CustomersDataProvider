using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;


namespace CustomersDataProvider.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomerInfoController : Controller
    {
        #region Fields

        private readonly IQueryParametersValidator _queryParametersValidator;
        private readonly ICustomerInfoServiceProvider _customerInfoServiceProvider;

        #endregion

        #region Constructors

        public CustomerInfoController(
            IQueryParametersValidator queryParametersValidator,
            ICustomerInfoServiceProvider customerInfoServiceProvider)
        {
            _queryParametersValidator = queryParametersValidator;
            _customerInfoServiceProvider = customerInfoServiceProvider;
        }

        #endregion

        #region Public

        // GET api/<controller>/get?customerID=1&email=bob@gmail.com
        [HttpGet("/get")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public async Task<IActionResult> Get([FromQuery]String customerID, [FromQuery]String email)
        {
            var criteria = new CustomerInfoCriteriaDTO
            {
                CustomerID = customerID,
                Email = email
            };

            if (!_queryParametersValidator.ValidateQueryParameters(criteria, out ICollection<String> errors))
                return BadRequest(errors);

            var customer = await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria)
                .ConfigureAwait(false);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        #endregion


    }
}