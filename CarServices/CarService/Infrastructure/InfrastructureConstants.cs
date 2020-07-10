namespace CarService.Infrastructure
{
   public class InfrastructureConstants
    {
        public const string AuthenticationCookieName = "Authentication";
        public const string AuthorizationHeaderName = "Authorization";
        public const string AuthorizationHeaderValuePrefix = "Bearer";
        public const string GarageAdminPolicyName = "Admin";
        public const string GarageSalesmanPolicyName = "Salesman";

        public const string GarageRoleHeaderName = "GarageRole";
        public const string CurrentGarageIdHeaderName = "CurrentGarageId";
        public const string CurrentEmployeeIdHeaderName = "CurrentEmployeeId";
        public const string CurrentEmployeeNameHeaderName = "CurrentEmployeeName";
    }
}
