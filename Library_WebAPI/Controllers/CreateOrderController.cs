using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateOrderController : Controller
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly ILogger<CreateOrderController> _logger;

        public CreateOrderController(ICreateOrderService createOrderService, ILogger<CreateOrderController> logger)
        {
            _createOrderService = createOrderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(NewOrderDTO newOrder)
        {
            var response=await  _createOrderService.CreateOrder(newOrder);
            if (response.Success)
            {
                _logger.LogInformation("book issued successfully");
                return Ok(response);
            }
            else if (response.Message.Contains("limit reached"))
            {
                _logger.LogError("Customer book issue has reached, no books can be issued ");
                return Forbid();
            }
            else if (response.Message.Contains("Customer not found"))
            {
                _logger.LogError("Customer not found ");
                return NotFound(response.Data);
            }
            else if (response.Message.Contains("staff not found"))
            {
                _logger.LogError("staff not found ");
                return Unauthorized(response.Data);
            }
            else
            {
                _logger.LogError("An error occurred while issuing book");
                return BadRequest();
            }
        }
    }
}
