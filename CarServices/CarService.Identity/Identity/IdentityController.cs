
namespace CarServices.Identity.Controllers
{
    using CarService.Identity.Identity.Models;
    using CarServices.Identity.Data.Models;
    using CarServices.Identity.Models;
    using CarServices.Identity.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    using static CarServices.Infrastructure.WebConstants;
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationSettings appSettings;
        private readonly IIdentityService identityService;

        public IdentityController(UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
            this.identityService = identityService;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<UserResponseModel>> Get(string id)
        {
            var result = await this.userManager.FindByIdAsync(id);

            return new UserResponseModel()
            {
                Id = result.Id,
                Name = result.UserName
            };
        }

        [HttpPost]
        [Route(nameof(Register))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Register(RegisterUserRequestModel registerUser)
        {
            var result = await this.identityService.RegisterAsync(registerUser);

            if (result.Succeeded)
            {
                return await Login(new LoginRequestModel(registerUser.UserName, registerUser.Password, true));
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseModel>> Create(RegisterUserRequestModel registerUser)
        {
            var result = await this.identityService.RegisterAsync(registerUser);

            if (result.Succeeded)
            {
                var newUser = await this.userManager.FindByNameAsync(registerUser.UserName);
                return new UserResponseModel()
                {
                    Id = newUser.Id,
                    Name = newUser.UserName,
                };
            }

            return BadRequest();
        }

        [HttpPost]
        [Route(nameof(Login))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel loginModel)
        {
            var result = await this.identityService.Login(loginModel, this.appSettings.Secret);

            return result is null ? Unauthorized() : (ActionResult<LoginResponseModel>)result;
        }
    }
}
