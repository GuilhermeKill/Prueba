using Microsoft.EntityFrameworkCore;
using Prueba.DB.Models;
using Shared;
using Shared.Dto;
using Shared.HttpRequests;

namespace Prueba.Repositories.Interfaces
{
    public interface ICarRepository
    {
        public IQueryable<CarModel> GetByFilters(FilterCarModel filter);
        public Task<bool> CreateCar(CarModel car);
        public Task<bool> DeleteCarById(int id);
        public Task<bool> UpdateCar(CarDto car);
    }
}
