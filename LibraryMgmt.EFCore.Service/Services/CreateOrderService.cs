using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Service.Services;
using LibraryMgmt.EFCore.Service.Services.Interfaces;

namespace LibraryMgmt.EFCore.DataAccess.Services
{
    public class CreateOrderService:ICreateOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly ICustomerRepository _custRepository;
        private readonly IUnitOfWork _UnitOfWork;
        public CreateOrderService(IOrderRepository orderRepository, IBookRepository bookRepository,IStaffRepository staffRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _OrderRepository = orderRepository;
            _bookRepository= bookRepository;
            _staffRepository= staffRepository;
            _custRepository= customerRepository;
            _UnitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<OrderDTO>> CreateOrder(NewOrderDTO orderDTO)
        {
            var response = new ServiceResponse<OrderDTO>();
            var staffDetails =await _staffRepository.GetStaffbyId(orderDTO.StaffId);
            var bookDetails =await _bookRepository.GetBookDetails(orderDTO.BookName);
            var CustomerDetails =await _custRepository.GetCustomerByPhoneNumber(orderDTO.CustomerPhoneNumber);
            if (staffDetails != null)
            {
                if (CustomerDetails != null)
                {
                    if (await _custRepository.GetIssuedBooksCount(CustomerDetails.CustomerId) <= 5)
                    {
                        Order order = new Order()
                        {
                            BookId = bookDetails.BookId,
                            CustomerId = CustomerDetails.CustomerId,
                            CustomerPhoneNumber = CustomerDetails.PhoneNumber,
                            StaffId = staffDetails.StaffId,
                            IssueDate = orderDTO.IssuedDate.Date,
                            DueDate = orderDTO.IssuedDate.Date.AddDays(15)
                        };
                        await _OrderRepository.Add(order);
                        await _bookRepository.UpdateBookingDetailsforBook(bookDetails.BookId);
                        await _UnitOfWork.Complete();
                        response.Data = new OrderDTO()
                        {
                            BookName = orderDTO.BookName,
                            CustomerName = CustomerDetails.CustomerName,
                            OrderId = order.OrderId
                        };
                        response.Success = true;
                        response.Message = "Order placed successfully";
                    }
                    else
                    {
                        response.Data = new OrderDTO() { };
                        response.Success = false;
                        response.Message = "Customer book issue limit reached";
                    }
                    
                }
                else
                {
                    response.Data = new OrderDTO() { };
                    response.Success = false;
                    response.Message = "Customer not found";
                }
            }
            else
            {
                response.Data = new OrderDTO() { };
                response.Success = false;
                response.Message = "staff not found";
            }
            return response;
        }
    }
}
