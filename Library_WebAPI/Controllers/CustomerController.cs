using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerCrudService _CustomerCrudService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerCrudService customerCrudService, ILogger<CustomerController> logger)
        {
            _CustomerCrudService = customerCrudService;
            _logger = logger;

        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDTO customer)
        {
            var response = await _CustomerCrudService.AddCustomer(customer);
            if (response.Success)
            {
                _logger.LogInformation("customer added successfully");
                return Ok();
            }
            else if (response.Message.Contains("Customer already exists! "))
            {
                return Forbid();
            }
            else
            {
                _logger.LogError("An error occurred while adding the customer");
                return BadRequest(response.Data);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response =await  _CustomerCrudService.GetAllCustomers();
            if (response.Success)
            {
                _logger.LogInformation("all customer(s) fetched successfully");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError("No customers available");
                return NotFound(response.Data);
            }
        }
    }
}
