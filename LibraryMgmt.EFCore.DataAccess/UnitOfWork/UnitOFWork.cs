using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.DataAccess.Repositories;
using LibraryMgmt.EFCore.Domain.Interfaces;

namespace LibraryMgmt.EFCore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context)
        {
            _context= context;
            Books = new BookRepository(_context);
            Staffs = new StaffRepository(_context);  
            Customers=new CustomerRepository(_context);
            Orders=new OrderRepository(_context);
        }

        public IBookRepository Books { get; private set; }

        public IStaffRepository Staffs { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
