using LibraryMgmt.EFCore.Service.DTOs;

namespace LibraryMgmt.EFCore.Service.Services.Interfaces
{
    public interface ICreateOrderService
    {
        Task<ServiceResponse<OrderDTO>> CreateOrder(NewOrderDTO orderDTO);
    }
}
