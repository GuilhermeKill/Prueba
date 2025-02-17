
using Prueba.DB.Models;
using Shared;
using Shared.Dto;
using Shared.HttpRequests;

namespace Prueba.Services.Interfaces
{
    public interface ICarServices
    {
        Task<Response> GetByFilters(FilterCarModel filters);
        Task<Response> CreateCar(CarDto car);
        Task<Response> DeleteCarById(int id);
        Task<Response> UpdateCar(CarDto car);

    }
}
