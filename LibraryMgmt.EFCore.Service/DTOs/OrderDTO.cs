using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.EFCore.Service.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string BookName { get; set; }    

        public string  CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime DueDate { get; set; }

    }
}


