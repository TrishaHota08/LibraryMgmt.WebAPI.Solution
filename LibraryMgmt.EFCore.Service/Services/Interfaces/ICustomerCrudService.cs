using LibraryMgmt.EFCore.Service.DTOs;

namespace LibraryMgmt.EFCore.Service.Services.Interfaces
{
    public interface ICustomerCrudService
    {
        Task<ServiceResponse<CustomerDTO>> AddCustomer(CustomerDTO customer);
        Task<CustomerDTO> GetCustomerByPhoneNumber(string phoneNumber);
        Task<ServiceResponse<IEnumerable<CustomerDTO>>> GetAllCustomers();
    }
}
