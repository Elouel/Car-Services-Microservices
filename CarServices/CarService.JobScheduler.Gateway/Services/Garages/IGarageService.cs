

namespace CarService.Gateway.Services.Garages
{
    using CarService.Features.Jobs.Models;
    using CarServices.Features.Vehicles;
    using CarServices.Gateway.Models;
    using Refit;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGarageService
    {
        [Get("/Employee/{id}")]
        Task<EmployeeResponseModel> Details(string id);

        [Get("/Department/{id}/Services")]
        Task<IEnumerable<PurchasedServiceModel>> Services(int id);


        [Get("/vehicles/all")]
        Task<IEnumerable<VehicleResponseModel>> Vehicles([Query(CollectionFormat.Multi)] IEnumerable<int> ids);
    }
}
