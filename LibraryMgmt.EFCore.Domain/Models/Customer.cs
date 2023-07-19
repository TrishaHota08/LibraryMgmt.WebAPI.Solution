namespace LibraryMgmt.EFCore.Domain.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }
        public int CustomerId { get; set; }

        public string PhoneNumber { get; set; }

        public string email { get; set; }
        public string CustomerName { get; set; }

        public List<Order>  Orders { get; set; }
    }
}
