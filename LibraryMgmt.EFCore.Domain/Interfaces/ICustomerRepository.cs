using LibraryMgmt.EFCore.Domain.Models;

namespace LibraryMgmt.EFCore.Domain.Interfaces
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        Task<int> GetIssuedBooksCount(int customerId);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> GetCustomerByPhoneNumber(string phoneNumber);
        Task<bool> IsCustomerExists(string PhoneNumber);

    }
}
