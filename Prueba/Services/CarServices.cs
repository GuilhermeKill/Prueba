using Microsoft.EntityFrameworkCore;
using Prueba.DB.Models;
using Prueba.Repositories.Interfaces;
using Prueba.Services.Interfaces;
using Shared;
using Shared.Dto;
using Shared.HttpRequests;

namespace Prueba.Services
{
    public class CarServices : ICarServices
    {
        private readonly ICarRepository _carRepository;

        public CarServices(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Response> GetByFilters(FilterCarModel filters)
        {
            var response = new Response();

            try
            {
                IQueryable<CarModel> query = _carRepository.GetByFilters(filters);

                if (filters.GetAllList)
                {
                    var cars = await query.ToListAsync();

                    response.Result = cars.Select(car => new CarDto
                    {
                        Id = car.Id,
                        country = car.country,
                        year = car.year,
                        model = car.model,
                        brand = car.brand,
                        codeVin = car.code_vin,
                        patent = car.patent
                    }).ToList();
                }
                else
                {
                    int totalRecords = await query.CountAsync();

                    var carsPaginated = await query
                        .Skip(Functions.SkipRows(filters.Page, filters.PageSize))
                        .Take(filters.PageSize)
                        .ToListAsync();

                    response.Result = new FilterResultDto<CarDto>
                    {
                        List = carsPaginated.Select(car => new CarDto
                        {
                            Id = car.Id,
                            country = car.country,
                            year = car.year,
                            model = car.model,
                            brand = car.brand,
                            codeVin = car.code_vin,
                            patent = car.patent
                        }).ToList(),
                        Pager = new PagerModel(totalRecords, filters.Page, filters.PageSize)
                    };
                }

                response.Status = 200;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response> CreateCar(CarDto car)
        {
            Response response = new Response();

            if (car.patent.Length > 8 || car.patent.Length < 6)
            {
                response.Status = 400;
                response.Message = "Patent must not be longer than 8 characters";

                return response;
            }

            if (car.codeVin.Length < 14 || car.codeVin.Length > 17)
            {
                response.Status = 400;
                response.Message = "code_vin must not be longer than 17 characters and less than 14";

                return response;
            }

            if (car.year > DateTime.Now.Year)
            {
                response.Status = 400;
                response.Message = "Not possible create Car with this year";

                return response;
            }

            var carModel = new CarModel()
            {
                country = car.country,
                year = car.year,
                patent = car.patent,
                code_vin = car.codeVin,
                brand = car.brand,
                model = car.model
            };

            try
            {
                var result = await _carRepository.CreateCar(carModel);

                response.Status = 200;
                response.Message = "car created successfully";
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Status = 400;
                response.Message = ex.Message;
                response.Result = false;
            }

            return response;
        }
        public async Task<Response> DeleteCarById(int id)
        {
            Response response = new Response();

            try
            {
                var result = await _carRepository.DeleteCarById(id);

                if (result)
                {
                    response.Message = "Car deleted";
                    response.Status = 200;
                }

                else
                {
                    response.Message = "car not found";
                    response.Status = 200;
                }
            }
            catch (Exception ex)
            {
                response.Status = 404;
                response.Message = ex.Message;
            }

            return response;

        }
        public async Task<Response> UpdateCar(CarDto car)
        {
            Response response = new Response();

            if (car.patent.Length > 8)
            {
                response.Status = 400;
                response.Message = "Patent must not be longer than 8 characters";

                return response;
            }

            if (car.codeVin.Length < 14 || car.codeVin.Length > 17)
            {
                response.Status = 400;
                response.Message = "code_vin must not be longer than 17 characters and less than 14";

                return response;
            }

            if (car.year > DateTime.Now.Year)
            {
                response.Status = 400;
                response.Message = "Not possible create Car with this year";

                return response;
            }

            try
            {
                var result = await _carRepository.UpdateCar(car);

                response.Status = 200;
                response.Message = "car update successfully";
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Status = 400;
                response.Message = ex.Message;
                response.Result = false;
            }

            return response;
        }
    }
}