using LibraryMgmt.EFCore.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryMgmt.EFCore.DataAccess.LibDbContext
{
    public sealed class LibraryDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Order> Orders { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = LibDB_final";
            optionsBuilder.UseSqlServer(connection, b => b.MigrationsAssembly("LibraryMgmt.WebAPI"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Staff)
                .HasForeignKey(x => x.StaffId);

            var intialStaffEntry = new Staff[]
            {
                new Staff{StaffId=1,StaffName="Admin", Email="admin@libraryuae.com", Password="admin123", StaffRole="Admin"},
                new Staff{StaffId=2,StaffName="Intern", Email="intern@libraryuae.com", Password="intern123", StaffRole="Intern"},
                new Staff{StaffId=3,StaffName="Librarian", Email="librarian@libraryuae.com", Password="librarian123", StaffRole="Librarian"}
                };

            modelBuilder.Entity<Staff>().HasData(intialStaffEntry);

            var intialBookEntry = new Book[]
            {
                new Book{BookId=1,Title="The Shining by Stephen King", BookingStatus="Available", Price=23},
                 new Book{BookId=2,Title="The Rise Of Titans", BookingStatus="Available", Price=100},
                  new Book{BookId=3,Title="IT by Stephen King", BookingStatus="Available", Price=14},
                   new Book{BookId=4,Title="The Notebook by Nicholas Sparks", BookingStatus="Available", Price=63}
            };
            modelBuilder.Entity<Book>().HasData(intialBookEntry);

            var initialCustomerEntry = new Customer[]
            {
                new Customer{CustomerId=1,CustomerName="Mathews Jon", email="mathews.jon@gmail.com", PhoneNumber="9867542310"},
                new Customer{CustomerId=2,CustomerName="Henry Williams", email="williams.hnry@gmail.com", PhoneNumber="9087564312"},
                new Customer{CustomerId=3,CustomerName="Edwards Snow", email="eddy.snow@gmail.com", PhoneNumber="9967578651"},
                new Customer{CustomerId=4,CustomerName="Willy Richard Mark", email="willy.richard@gmail.com", PhoneNumber="9876504321"}
            };
            modelBuilder.Entity<Customer>().HasData(initialCustomerEntry);

            

        }

        

    }
}
