using Shared.HttpRequests;

namespace Prueba.Services.Interfaces
{
    public interface ICountryService
    {
        Task<Response> GetCountries();
        Task<Response> ConsumeApi();
    }
}
