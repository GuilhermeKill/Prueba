using Shared;
using Shared.Dto;
using Shared.HttpRequests;


namespace MVC.ViewModels.Interfaces
{
    public interface ICarViewModel
    {
        Task<Response> GetByFilters(FilterCarModel user);
        Task<Response> DeleteCarByID(int id);
        Task<Response> EditCar(CarDto car); 
        Task<Response> CreateCar(CarDto car);
    }
}

