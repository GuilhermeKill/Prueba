using Shared.HttpUtils;
using Shared.HttpRequests;
using Shared.HttpUtils;
using Shared.Dto;
using Shared.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Shared.Services
{
    public class CarService : ICarService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;

        public CarService(IOptions<HostService> service)
        { 
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }

       

        public async Task<Response> GetByFilters(FilterCarModel car) => await RestUtility.WebServiceAsync
            ($"{_host.CarEndPoint}/GetByFilters",
                string.Empty,
                car,
                "POST",
                string.Empty,
                string.Empty);

        public async Task<Response> DeleteById(int id) => await RestUtility.WebServiceAsync
           ($"{_host.CarEndPoint}/DeleteCarById/{id}",
               string.Empty,
               null,
               "DELETE",
               string.Empty,
               string.Empty);

        public async Task<Response> EditCar(CarDto car) => await RestUtility.WebServiceAsync
           ($"{_host.CarEndPoint}/UpdateCar",
               string.Empty,
               car,
               "PUT",
               string.Empty,
               string.Empty);

        public async Task<Response> CreateCar(CarDto car) => await RestUtility.WebServiceAsync
          ($"{_host.CarEndPoint}/CreateCar",
              string.Empty,
              car,
              "POST",
              string.Empty,
              string.Empty);
    }
}
