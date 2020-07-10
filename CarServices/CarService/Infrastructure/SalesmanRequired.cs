
namespace CarServices.Garage.Infrastructure
{
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    public class SalesmanRequired : IAuthorizationRequirement
    {
    }

    public class SalesmanRequiredHandler : AuthorizationHandler<SalesmanRequired>
    {
        private readonly ICurrentEmployeeService currentEmployeeSerivce;

        public SalesmanRequiredHandler(ICurrentEmployeeService currentEmployeeSerivce)
        {
            this.currentEmployeeSerivce = currentEmployeeSerivce;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SalesmanRequired requirement)
        {
            if (currentEmployeeSerivce.IsSalesman)
            {
                context.Succeed(requirement);
            }
        }
    }
}
