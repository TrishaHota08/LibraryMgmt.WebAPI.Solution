using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Service.Services;
using LibraryMgmt.EFCore.Service.Services.Interfaces;

namespace LibraryMgmt.EFCore.Service.Services
{
    public class OrderCrudService : IOrderCrudService
    {
        private readonly IOrderRepository _OrderRepository;
        public OrderCrudService(IOrderRepository orderRepository)
        {
            _OrderRepository = orderRepository; 
        }

        public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var response= new ServiceResponse<IEnumerable<OrderDTO>>();
            var Orders = await _OrderRepository.GetAll();
            if (Orders.Count() != 0)
            {
                response.Data= Orders.Select
                    (x => new OrderDTO()
                    {
                        OrderId=x.OrderId,
                        CustomerName = x.Customer.CustomerName,
                        BookName = x.Book.Title,
                        CustomerPhoneNumber=x.CustomerPhoneNumber,
                        IssuedDate = x.IssueDate.Date,
                        DueDate = x.DueDate.Date

                    }
                    );
                response.Success = true;
                response.Message = "All orders fetched";
            }
            else
            {
                response.Data= Enumerable.Empty<OrderDTO>();
                response.Success = false;
                response.Message = "No orders found";
            }
            return response;
        }
    }
}
