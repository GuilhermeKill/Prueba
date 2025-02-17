using MVC.ViewModels.Interfaces;
using Shared.HttpRequests;
using Shared.Services.Interfaces;

namespace MVC.ViewModels
{
    public class CountryViewModel : ICountryViewModel
    {
        private readonly ICountryService _countryService;

        public CountryViewModel(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<Response> GetCountries() => await _countryService.GetCountries();
    }
}
