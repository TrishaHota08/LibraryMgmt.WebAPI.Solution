using LibraryMgmt.EFCore.Service.DTOs;

namespace LibraryMgmt.EFCore.Service.Services.Interfaces
{
    public interface IBookCrudService
    {
        Task<ServiceResponse<IEnumerable<BookDTO>>> GetAllBooks();
        Task<ServiceResponse<IEnumerable<BookDTO>>> GetAvailableBooks();
        Task<ServiceResponse<BookDTO>> AddBook(BookDTO newBook);
        Task<bool> DeleteBook(string name);
    }
}