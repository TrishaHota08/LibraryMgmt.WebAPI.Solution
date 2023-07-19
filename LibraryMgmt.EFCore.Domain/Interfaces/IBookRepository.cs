using LibraryMgmt.EFCore.Domain.Models;

namespace LibraryMgmt.EFCore.Domain.Interfaces
{
    public interface IBookRepository:IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAvailableBooks();
        Task<Book> AddBook(Book book);
        Task<bool> IsBookExists(string title);
        Task<Book> GetBookDetails(string bookTitle);
        Task UpdateBookingDetailsforBook(int bookId);

    }
}
