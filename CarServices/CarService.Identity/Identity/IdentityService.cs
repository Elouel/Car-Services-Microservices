namespace CarServices.Identity.Services
{
    using System;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using CarServices.Identity.Data.Models;
    using CarServices.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;

        public IdentityService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserRequestModel registerUser)
        {
            var user = new User()
            {
                Email = registerUser.Email,
                UserName = registerUser.UserName
            };

            return await this.userManager.CreateAsync(user, registerUser.Password);
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginModel, string secret)
        {
            var user = await this.userManager.FindByNameAsync(loginModel.UserName);
            if (user == null)
            {
                return null;
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!passwordValid)
            {
                return null;
            }

            var token = this.GenerateJwtToken(
                user.Id,
                user.UserName,
                secret);


            return new LoginResponseModel(token, user.Id, loginModel.IsCurrentlyRegistered);
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
