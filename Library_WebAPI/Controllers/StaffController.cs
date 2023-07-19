using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffCrudService _StaffCrudService;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IStaffCrudService staffCrudService, ILogger<StaffController> logger)
        {
            _StaffCrudService = staffCrudService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetStaff(int staffId)
        {
            var response =await _StaffCrudService.GetStaff(staffId);
            if (response.Success)
            {
                _logger.LogInformation("staff details fetched successfully");
                return Ok(response.Data);
            }
            _logger.LogError("ano staff found");
            return NotFound(response.Data);
           
        }
    }
}
