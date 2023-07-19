using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgmt.EFCore.DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context):base(context)
        {

        }
        public async  Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return _libraryDbContext.Books.Where(b=>b.BookingStatus=="Available").OrderBy(b => b.Title).ToList();
        }

        public async override Task<IEnumerable<Book>> GetAll()
        {
            return await _libraryDbContext.Books.ToListAsync();

        }

        public async Task<Book> AddBook(Book book)
        {
             await _libraryDbContext.Books.AddAsync(book);
            return book;

        }

        public async Task<bool> IsBookExists(string title)
        {
           return await  _libraryDbContext.Books
           .AsNoTracking()
           .AnyAsync(x => x.Title == title);
        }

        public async Task<Book> GetBookDetails(string bookTitle)
        {
            var bookdetails= await _libraryDbContext.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b=>b.Title==bookTitle);
            return bookdetails;
        }

        public async Task UpdateBookingDetailsforBook(int bookId)
        {

            var book =await  _libraryDbContext.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book != null)
            {
                book.BookingStatus = "NotAvailable";

                _libraryDbContext.Books.Update(book);
            }

        }
    }
}
