namespace CarServices.Garage.Infrastructure.Extensions
{
    using CarServices.Garage.Features.Garages.Models;
    using Microsoft.AspNetCore.Authorization;

    public static class AuthorizationPolicyBuilderExtensions
    {
        ////public static AuthorizationPolicyBuilder RequireCustomClaim(
        ////    this AuthorizationPolicyBuilder builder,
        ////    RoleType roleType)
        ////{
        ////    builder.AddRequirements(new AdminRequired(roleType));
        ////    return builder;
        ////}
    }
}