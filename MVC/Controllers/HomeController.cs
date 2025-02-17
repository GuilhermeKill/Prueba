using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels.Interfaces;
using Newtonsoft.Json;
using Shared;
using Shared.Dto;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarViewModel _carViewModel;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICarViewModel carViewModel)
        {
            _carViewModel = carViewModel;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GridCars()
        {
            string draw = string.Empty;
            int recordsTotal = 0;
            var data = new List<CarDto>();

            try
            {
                draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var countryFilter = Request.Form["countryFilter"].FirstOrDefault();
                var yearFilter = Request.Form["yearFilter"].FirstOrDefault();


                var filters = new FilterCarModel
                {
                    Year = yearFilter != "" ? Convert.ToInt32(yearFilter) : 0,
                    Page = ((start != null ? Convert.ToInt32(start) : 0) / (length != null ? Convert.ToInt32(length) : 10)) + 1,
                    PageSize = length != null ? Convert.ToInt32(length) : 10,
                    SearchByText = searchValue,
                    Country = countryFilter == null ? "" : countryFilter
                };


                var response = await _carViewModel.GetByFilters(filters);

                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<FilterResultDto<CarDto>>(response.Result.ToString());
                    recordsTotal = result.Pager.TotalItems;
                    data = result.List;
                }


            }
            catch (Exception ex)
            {


            }

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            });
        }

        public async Task<IActionResult> DeleteCar(int id)
        {

            var response = await _carViewModel.DeleteCarByID(id);

            return Json(response);
        }

        public async Task<IActionResult> CreateCar([FromBody] CarDto car)
        {
            var response = await _carViewModel.CreateCar(car);

            return Json(response);
        }

        public async Task<IActionResult> UpdateCar([FromBody] CarDto car)
        {

            var response = await _carViewModel.EditCar(car);

            return Json(response);
        }


    }
}
