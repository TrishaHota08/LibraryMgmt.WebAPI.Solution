

using LibraryMgmt.EFCore.Domain.Models;

namespace LibraryMgmt.EFCore.Domain.Interfaces 
{
    public interface IUserLoginRepository
    {
        Task<Staff> Login(string user, string password );
        
    }
}
