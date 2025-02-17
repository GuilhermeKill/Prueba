using Prueba.Repositories.Interfaces;
using Prueba.Services.Interfaces;
using Shared.HttpRequests;

namespace Prueba.Services
{
    public class CountryService : ICountryService
    {
        private readonly IContryRepository _countryRepository;

        public CountryService(IContryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Response> GetCountries()
        {
            Response response = new Response();

            response.Result = await _countryRepository.GetCountries();

            return response;
        }
        public async Task<Response> ConsumeApi()
        {
            Response response = new Response();

            var result = await _countryRepository.ConsumeApi();

            return response;
        }
    }
}
