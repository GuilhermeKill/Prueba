using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels.Interfaces;
using Newtonsoft.Json;
using Shared.Dto;

namespace MVC.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryViewModel _countryViewModel;
        private readonly ILogger<HomeController> _logger;

        public CountryController(ILogger<HomeController> logger, ICountryViewModel countryViewModel)
        {
            _countryViewModel = countryViewModel;
            _logger = logger;
        }


        public async Task<IActionResult> ConsumeContries()
        {
            var reseponse = await _countryViewModel.GetCountries();

            var result = JsonConvert.DeserializeObject<List<CountryDto>>(reseponse.Result.ToString());
            return Json(result);
        }

    }
}
