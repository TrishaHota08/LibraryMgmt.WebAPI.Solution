using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookCrudService _BookCrudService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookCrudService bookCrudService, ILogger<BookController> logger)
        {
            _BookCrudService = bookCrudService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableBook()
        {
            var response = await _BookCrudService.GetAvailableBooks();
            if(response.Success)
            {
                _logger.LogInformation("Fetched the available books");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError("An error occurred as no books are available.");
                return NotFound(response.Data);
            }
        }

        [HttpGet("allbooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var response =await  _BookCrudService.GetAllBooks();
            if (response.Success)
            {
                _logger.LogInformation("Fetched all the books");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError("An error occurred as no books are in stock.");
                return NotFound(response.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO newBookDTO)
        {
            var response = await _BookCrudService.AddBook(newBookDTO);
            if(response.Success)
            {
                _logger.LogInformation("book added successfully");
                return Ok(response.Data);
            }
            else if(response.Message.Contains("Book already exists!"))
            {
                return Forbid();
            }
            else
            {
                _logger.LogError("An error occurred while adding the book");
                return BadRequest(response.Data);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string bookName)
        {
            bool IsDeleted=await _BookCrudService.DeleteBook(bookName);
            if(IsDeleted)
            {
                _logger.LogInformation("book deleted successfully");
                return Ok("Deleted successfully");
            }
            else
            {
                _logger.LogError("An error occurred while deleting the book");
                return BadRequest();
            }
        }
        
    }
}
