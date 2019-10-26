using System;
using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomersDataPovider.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICustomerInfoServiceProvider _customerInfoServiceProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICustomerInfoServiceProvider customerInfoServiceProvider)
        {
            _logger = logger;
            _customerInfoServiceProvider = customerInfoServiceProvider;
        }

        [HttpGet]
        public async Task<String> Get()
        {
            var criteria = new CustomerInfoCriteriaDTO() 
            { 
                CustomerID = "1"
            };
            var customer =  await _customerInfoServiceProvider.GetCustomerInfoAsync(criteria).ConfigureAwait(false);
            return customer.ToString();
            /*var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}
