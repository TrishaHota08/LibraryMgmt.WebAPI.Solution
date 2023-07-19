using LibraryMgmt.EFCore.DataAccess;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryMgmt.EFCore.Service.Services
{
    public class UserLoginJWTokenService : IUserLoginJWTokenService
    {
        private readonly IUserLoginRepository _jWTManagerRepository;
        private readonly IConfiguration _configuration;
        public UserLoginJWTokenService(IUserLoginRepository jWTManagerRepository, IConfiguration configuration)
        {
            _jWTManagerRepository = jWTManagerRepository;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<UserAuthTokenDTO>> Login(LoginCredDTO creds)
        {
           var response=new ServiceResponse<UserAuthTokenDTO>();
            var staffDetails =await  _jWTManagerRepository.Login(creds.Email, creds.Password);
            if (staffDetails != null) {
                response.Data = new UserAuthTokenDTO()
                {
                    UserName= staffDetails.StaffName,
                    Role = staffDetails.StaffRole,
                    Email=staffDetails.Email,
                    AccessToken=GenerateToken(staffDetails.Email)
                };
                response.Success=true;
                response.Message = "User credentials matched, generated Token";
            }
            else
            {
                response.Data = null;
                response.Success=false;
                response.Message = "No staff found with provided credentials";
            }
            return response;

        }

        public string GenerateToken(string email)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, email)
            };

            var symmetricKey = SecurityKeyGenerator.GetSymmetricSecurityKey(_configuration);

            SigningCredentials creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
