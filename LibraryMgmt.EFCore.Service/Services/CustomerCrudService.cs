using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Service.Services;
using LibraryMgmt.EFCore.Service.Services.Interfaces;

namespace LibraryMgmt.EFCore.Service.Services
{
    public class CustomerCrudService : ICustomerCrudService
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerCrudService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _CustomerRepository= customerRepository;
            _unitOfWork= unitOfWork;
        }
        public async Task<ServiceResponse<CustomerDTO>> AddCustomer(CustomerDTO customer)
        {
            var response = new ServiceResponse<CustomerDTO>();
            if (!await _CustomerRepository.IsCustomerExists(customer.PhoneNumber))
            {
                var cust = await _CustomerRepository.AddCustomer(ConvertCustomerDTOToCustomer(customer));
                await _unitOfWork.Complete();
                response.Data= new CustomerDTO()
                {
                    CustomerName = cust.CustomerName,
                    PhoneNumber = cust.PhoneNumber,
                    Email = cust.email
                };
                response.Success = true;
                response.Message = "Customer Added successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Customer already exists! ";
            }
            return response;
        }

        public async Task<CustomerDTO> GetCustomerByPhoneNumber(string phoneNumber)
        {
            var cust = await _CustomerRepository.GetCustomerByPhoneNumber(phoneNumber);
            if (cust != null)
            {
                return new CustomerDTO()
                {
                    CustomerName= cust.CustomerName,
                    PhoneNumber = cust.PhoneNumber,
                    Email = cust.email
                };
            }
            return null;
        }

        public async Task<ServiceResponse<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var response = new ServiceResponse<IEnumerable<CustomerDTO>>();
            var customers = await _CustomerRepository.GetAll();
            if (customers.Count() != 0)
            {
                response.Data= customers.Select
                    (x => new CustomerDTO()
                    {
                        CustomerName= x.CustomerName,
                        PhoneNumber= x.PhoneNumber,
                        Email= x.email
                    }
                    );
                response.Success = true;
                response.Message = "Customers fetched successfully";
            }
            else
            {
                response.Data= Enumerable.Empty<CustomerDTO>();
                response.Success = false;
                response.Message = "No customers found";
            }
            return response;
        }
        private Customer ConvertCustomerDTOToCustomer(CustomerDTO cust)
        {
            return new Customer()
            {
                CustomerName = cust.CustomerName,
                PhoneNumber=cust.PhoneNumber,
                email=cust.Email
            };
        }
    }
}
