using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.EFCore.Service.DTOs
{
    public class NewOrderDTO
    {
        public int StaffId { get; set; }
        public string BookName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
