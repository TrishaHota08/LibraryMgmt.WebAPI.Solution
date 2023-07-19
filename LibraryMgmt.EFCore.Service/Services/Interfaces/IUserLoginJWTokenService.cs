using LibraryMgmt.EFCore.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.EFCore.Service.Services.Interfaces
{
    public interface IUserLoginJWTokenService
    {
        Task<ServiceResponse<UserAuthTokenDTO>> Login(LoginCredDTO creds);
    }
}
