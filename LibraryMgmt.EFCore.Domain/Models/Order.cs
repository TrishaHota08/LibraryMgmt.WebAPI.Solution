using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMgmt.EFCore.Domain.Models
{
    public class Order
    {
        
        public int OrderId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public string CustomerPhoneNumber { get; set; }
        public Staff Staff { get; set; }
        public int StaffId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
