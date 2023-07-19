using LibraryMgmt.EFCore.Domain.Models;

namespace LibraryMgmt.EFCore.Domain.Interfaces
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task AddOrder(Order order);
    }
}
