using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgmt.EFCore.DataAccess.Repositories
{
    public class CustomerRepository:GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(LibraryDbContext context):base(context)
        {
            
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            await _libraryDbContext.Customers.AddAsync(customer);
            return customer;
        }

        public async Task<Customer> GetCustomerByPhoneNumber(string phoneNumber)
        {
            var cust = await _libraryDbContext.Customers.FirstOrDefaultAsync(c=>c.PhoneNumber == phoneNumber);
            if (cust != null)
            {
                return cust;
            }
            return null;
        }

        public async Task<int> GetIssuedBooksCount(int customerId)
        {
            return await _libraryDbContext.Customers.Where(c => c.CustomerId == customerId)
                   .Include(c => c.Orders).CountAsync();
        }

        public async Task<bool> IsCustomerExists(string phoneNumber)
        {
            return await _libraryDbContext.Customers
            .AsNoTracking()
            .AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
