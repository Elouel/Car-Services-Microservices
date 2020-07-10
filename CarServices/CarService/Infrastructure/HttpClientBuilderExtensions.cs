namespace CarRentalSystem.Infrastructure
{
    using System;
    using System.Net.Http.Headers;
    using CarService.Services.Identity;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    using static CarService.Infrastructure.InfrastructureConstants;

    public static class HttpClientBuilderExtensions
    {
        public static void WithConfiguration(
            this IHttpClientBuilder httpClientBuilder,
            string baseAddress)
            => httpClientBuilder
                .ConfigureHttpClient((serviceProvider, client) =>
                {
                    client.BaseAddress = new Uri(baseAddress);

                    var requestServices = serviceProvider
                        .GetService<IHttpContextAccessor>()
                        ?.HttpContext
                        .RequestServices;

                    var currentToken = requestServices
                        ?.GetService<ICurrentTokenService>()
                        ?.Get();

                    if (currentToken == null)
                    {
                        return;
                    }

                    ////var currentEmployeeService = requestServices
                    ////    ?.GetService<ICurrentEmployeeService>();

                    var authorizationHeader = new AuthenticationHeaderValue(AuthorizationHeaderValuePrefix, currentToken);
                    client.DefaultRequestHeaders.Authorization = authorizationHeader;
                    ////client.DefaultRequestHeaders.Add(GarageRoleHeaderName, currentEmployeeService.GarageRole.ToString());
                    ////client.DefaultRequestHeaders.Add(CurrentGarageIdHeaderName, currentEmployeeService.CurrentGarageId.ToString());
                    ////client.DefaultRequestHeaders.Add(CurrentEmployeeIdHeaderName, currentEmployeeService.CurrentEmployeeId.ToString());
                    ////client.DefaultRequestHeaders.Add(CurrentEmployeeNameHeaderName, currentEmployeeService.EmployeeName);
                });
    }
}
