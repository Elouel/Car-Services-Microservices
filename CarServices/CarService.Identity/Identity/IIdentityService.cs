using CarServices.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CarServices.Identity.Services
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(RegisterUserRequestModel registerUser);

        Task<LoginResponseModel> Login(LoginRequestModel loginModel, string secret);

        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
