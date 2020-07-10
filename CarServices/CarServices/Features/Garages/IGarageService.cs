
namespace CarServices.Garage.Features.Garages
{
    using CarServices.Garage.Features.Garages.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface IGarageService
    {
        Task<Data.Models.Garage> GetGarage(int garageId);

        Task<Data.Models.Garage> GetDefaultGarage(string userId);

        Task<IList<Data.Models.Department>> GetGarageDepartments(int garageId);

        Task<GarageResponseModel> CreateGarage(GarageCreateRequestModel model, string userId);
    }
}
