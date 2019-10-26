using BusinessLogicLayer.DataTransferObjects;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstraction
{
    public interface ICustomerInfoServiceProvider
    {

        Task<CustomerDTO> GetCustomerInfoAsync(CustomerInfoCriteriaDTO criteria);

    }
}
