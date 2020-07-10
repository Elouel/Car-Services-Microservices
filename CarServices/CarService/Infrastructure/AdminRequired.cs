
namespace CarServices.Garage.Infrastructure
{
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    public class AdminRequired : IAuthorizationRequirement
    {
    }

    public class AdminRequiredHandler : AuthorizationHandler<AdminRequired>
    {
        private readonly ICurrentEmployeeService currentEmployeeSerivce;

        public AdminRequiredHandler(ICurrentEmployeeService currentEmployeeSerivce)
        {
            this.currentEmployeeSerivce = currentEmployeeSerivce;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AdminRequired requirement)
        {
            if (currentEmployeeSerivce.IsAdmin)
            {
                context.Succeed(requirement);
            }
        }
    }
}
