using Microsoft.Extensions.Options;
using Shared.HttpRequests;
using Shared.HttpUtils;
using Shared.Services.Interfaces;

namespace Shared.Services
{
    public class CountryService : ICountryService
    {
        private readonly HostService _service;
        private readonly RestApiEndPoints _host;
        public CountryService(IOptions<HostService> service)
        {
            _service = service.Value;
            _host = new RestApiEndPoints(_service);
        }

        public async Task<Response> GetCountries() => await RestUtility.WebServiceAsync
            ($"{_host.CountryEndPoint}/GetCountries",
                string.Empty,
                null,
                "GET",
                string.Empty,
                string.Empty);
    }
}
