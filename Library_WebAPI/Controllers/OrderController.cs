using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderCrudService _orderCrudService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderCrudService orderCrudService, ILogger<OrderController> logger)
        {
            _orderCrudService = orderCrudService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var response=await _orderCrudService.GetAllOrders();
            if(response.Success)
            {
                _logger.LogInformation("all orders fetched successfully");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError("no orders fetched");
                return NotFound(response.Data);
            }

        }
    }
}
