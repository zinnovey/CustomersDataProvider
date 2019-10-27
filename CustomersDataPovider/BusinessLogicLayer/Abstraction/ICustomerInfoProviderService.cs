using System.Threading.Tasks;
using CustomersDataProvider.BusinessLogicLayer.DataTransferObjects;

namespace CustomersDataProvider.BusinessLogicLayer.Abstraction
{
    public interface ICustomerInfoProviderService
    {

        Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria);

    }
}
