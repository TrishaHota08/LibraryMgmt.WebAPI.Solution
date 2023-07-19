using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Service.Services;
using LibraryMgmt.EFCore.Service.Services.Interfaces;

namespace LibraryMgmt.EFCore.Service.Services
{
    public sealed class BookCrudService : IBookCrudService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BookCrudService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<BookDTO>> AddBook(BookDTO newBook)
        {
            var response = new ServiceResponse<BookDTO>();
            if (!await _bookRepository.IsBookExists(newBook.Title))
            {

                var book = ConvertDTOToModel(newBook);
                var addedBook = await _bookRepository.AddBook(book);
                await _unitOfWork.Complete();
                response.Data=new BookDTO()
                {
                    Title = addedBook.Title,
                    BookStatus = addedBook.BookingStatus,
                    Price = addedBook.Price
                };
                response.Success = true;
                response.Message = "Book added successfully";

            }
            else
            {
                response.Success = false;
                response.Message = "Book already exists! ";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<BookDTO>>> GetAllBooks()
        {
            var response = new ServiceResponse<IEnumerable<BookDTO>>();
            var books = await _bookRepository.GetAll();
            if (books.Count() != 0)
            {
                response.Data = books.Select(x =>
                new BookDTO()
                {
                    Title = x.Title,
                    BookStatus = x.BookingStatus,
                    Price = x.Price
                });
                response.Success = true;
                response.Message = "Books fetched successfully";
            }
            else
            {
                response.Data=Enumerable.Empty<BookDTO>();
                response.Success = false;
                response.Message = "No books in stock";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<BookDTO>>> GetAvailableBooks()
        {
            var response = new ServiceResponse<IEnumerable<BookDTO>>();
            var books = await _bookRepository.GetAvailableBooks();
            if (books.Count() != 0)
            {
                response.Data = books.Select(x =>
                new BookDTO()
                {
                    BookId=x.BookId,
                    Title = x.Title,
                    BookStatus = x.BookingStatus,
                    Price = x.Price
                });
                response.Success = true;
                response.Message = "Books fetched successfully";
            }
            else
            {
                response.Data = Enumerable.Empty<BookDTO>();
                response.Success = false;
                response.Message = "No books available for issuing";
            }
            return response;

        }

        public async Task<bool> DeleteBook(string name)
        {
            var book = await _bookRepository.GetBookDetails(name);
            if (book != null)
            {
                if (book.BookingStatus == "Available")
                {
                    _bookRepository.Remove(book);
                    await _unitOfWork.Complete();
                    return true;
                }
            }
            return false;
        }

        private Book ConvertDTOToModel(BookDTO bookDTO)
        {
            return new Book()
            {
                Title = bookDTO.Title,
                BookingStatus = bookDTO.BookStatus,
                Price = bookDTO.Price
            };
        }

    }
}
