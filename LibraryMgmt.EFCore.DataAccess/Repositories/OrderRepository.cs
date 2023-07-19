using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace LibraryMgmt.EFCore.DataAccess.Repositories
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LibraryDbContext context):base(context)
        {
            
        }
        public async Task AddOrder(Order order)
        {
            await _libraryDbContext.Orders.AddAsync(order);

        }

        public async override Task<IEnumerable<Order>> GetAll()
        {
           var orders= await _libraryDbContext.Orders.Include(o=>o.Customer)
                .Include(c=>c.Book)
                .ToListAsync();
            return orders;
        }

    }
}
