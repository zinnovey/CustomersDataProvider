using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;

namespace CustomersDataProvider.BusinessLogicLayer.Abstraction
{
    public interface ICustomerInfoServiceProvider
    {

        Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria);

    }
}
