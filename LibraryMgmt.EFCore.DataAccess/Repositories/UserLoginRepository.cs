using LibraryMgmt.EFCore.DataAccess;
using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryMgmt.WebAPI.JWTAuthenticationAuthorization.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly LibraryDbContext _context;
        public UserLoginRepository(LibraryDbContext context)
        {
            _context = context;

        }
        public async Task<Staff> Login(string email, string password)
        {
            var loginCheck = await _context.Staffs.Where(s => s.Email == email && s.Password == password).FirstOrDefaultAsync();
            if (loginCheck != null)
            {
                return loginCheck;
            }
            else
            {
                return null;
            }
        }
    }
}
