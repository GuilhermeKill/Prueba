using Microsoft.EntityFrameworkCore;
using Prueba.DB;
using Prueba.DB.Models;
using Prueba.Repositories.Interfaces;
using Shared;
using Shared.Dto;

namespace Prueba.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<bool> CreateCar(CarModel car)
        {
            _context.Add(car);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCarById(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<CarModel> GetByFilters(FilterCarModel filters)
        {

            var query = _context.Cars.AsNoTracking();

            if (filters != null)
            {
                if (!string.IsNullOrEmpty(filters.Country))
                {
                    query = query.Where(e => e.country == filters.Country);
                }

                if (filters.Year != 0)
                {
                    query = query.Where(e => e.year == filters.Year);
                }

                if (!string.IsNullOrEmpty(filters.SearchByText))
                {
                    query = query.Where(e => e.brand.ToLower().Contains(filters.SearchByText.ToLower()) ||
                    e.code_vin.ToLower().Contains(filters.SearchByText.ToLower()) ||
                    e.model.ToLower().Contains(filters.SearchByText.ToLower()) ||
                    e.patent.ToLower().Contains(filters.SearchByText.ToLower()));
                }
            }

            return query;
        }

        public async Task<bool> UpdateCar(CarDto car)
        {
            var query = _context.Cars.AsNoTracking().Where(e => e.Id == car.Id);

            var existingCar = await query.FirstOrDefaultAsync();

            if (existingCar == null)
            {
                return false;
            }

            var carToUpdate = new CarModel
            {
                Id = existingCar.Id,
                country = !string.IsNullOrEmpty(car.country) ? car.country : existingCar.country,
                year = car.year != 0 ? car.year : existingCar.year,
                model = !string.IsNullOrEmpty(car.model) ? car.model : existingCar.model,
                brand = !string.IsNullOrEmpty(car.brand) ? car.brand : existingCar.brand,
                code_vin = !string.IsNullOrEmpty(car.codeVin) ? car.codeVin : existingCar.code_vin,
                patent = !string.IsNullOrEmpty(car.patent) ? car.patent : existingCar.patent
            };

            _context.Cars.Update(carToUpdate);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
