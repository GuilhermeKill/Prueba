using Shared.Dto;
using Shared.HttpUtils;
using System.Threading.Tasks;
using Shared.HttpRequests;

namespace Shared.Services.Interfaces
{
    public interface ICarService
    {
        Task<Response> GetByFilters(FilterCarModel car);
        Task<Response> DeleteById(int id);
        Task<Response> EditCar(CarDto car);
        Task<Response> CreateCar(CarDto car);
    }
}
