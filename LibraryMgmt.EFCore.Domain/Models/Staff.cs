namespace LibraryMgmt.EFCore.Domain.Models
{
    public class Staff
    {
        public Staff()
        {
            Orders = new List<Order>();
        }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffRole { get; set; }
        public List<Order> Orders { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
