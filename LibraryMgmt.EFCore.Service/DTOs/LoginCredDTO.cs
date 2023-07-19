using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.EFCore.Service.DTOs
{
    public class LoginCredDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
