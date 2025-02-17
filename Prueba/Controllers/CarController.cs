using Microsoft.AspNetCore.Mvc;
using Prueba.DB.Models;
using Prueba.Services.Interfaces;
using Shared;
using Shared.Dto;
namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        public readonly ICarServices _carServices;

        public CarController(ICarServices carInterface)
        {
            _carServices = carInterface;
        }

        [HttpPost("GetByFilters")]
        public async Task<IActionResult> GetByFilters(FilterCarModel filters) => Ok(await _carServices.GetByFilters(filters));

        [HttpPost("CreateCar")]
        public async Task<IActionResult> CreateCar(CarDto car) => Ok(await _carServices.CreateCar(car));

        [HttpDelete("DeleteCarById/{id}")]
        public async Task<IActionResult> DeleteCarById(int id) => Ok(await _carServices.DeleteCarById(id)); 
        [HttpPut("UpdateCar")]
        public async Task<IActionResult> UpdateCar(CarDto car) => Ok(await _carServices.UpdateCar(car));

       
    }
}
