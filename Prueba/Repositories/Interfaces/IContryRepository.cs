using Prueba.DB.Models;
using System.Threading.Tasks;

namespace Prueba.Repositories.Interfaces
{
    public interface IContryRepository
    {
        public Task<bool> ConsumeApi();
        public Task<List<CountryModel>> GetCountries();
    }
}
