using LibraryMgmt.EFCore.Service.DTOs;

namespace LibraryMgmt.EFCore.Service.Services.Interfaces
{
    public interface IOrderCrudService
    {
        Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrders();


    }
}
