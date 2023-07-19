namespace LibraryMgmt.EFCore.Domain.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string BookingStatus { get; set; }
        public int Price { get; set; }
        
    }
}
