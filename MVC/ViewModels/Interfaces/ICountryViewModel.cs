using Shared.HttpRequests;

namespace MVC.ViewModels.Interfaces
{
    public interface ICountryViewModel
    {
        Task<Response> GetCountries();
    }
}
