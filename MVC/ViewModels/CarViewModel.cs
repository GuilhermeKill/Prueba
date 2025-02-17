using MVC.ViewModels.Interfaces;
using Shared;
using Shared.Dto;
using Shared.HttpRequests;
using Shared.Services.Interfaces;

namespace MVC.ViewModels
{
    public class CarViewModel : ICarViewModel
    {

        private readonly ICarService _carService;

        public CarViewModel(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<Response> GetByFilters(FilterCarModel cars) => await _carService.GetByFilters(cars);

        public async Task<Response> DeleteCarByID(int id) => await _carService.DeleteById(id);

        public async Task<Response> EditCar(CarDto car) => await _carService.EditCar(car);

        public  async Task<Response> CreateCar(CarDto car) => await _carService.CreateCar(car);
    }
}
