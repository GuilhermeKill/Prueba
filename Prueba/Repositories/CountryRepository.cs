using Microsoft.EntityFrameworkCore;
using Prueba.DB;
using Prueba.DB.Models;
using Prueba.Repositories.Interfaces;
using System.Text.Json;

namespace Prueba.Repositories
{
    public class CountryRepository : IContryRepository
    {
        private readonly AppDbContext _context;
        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<CountryModel>();
            }
        }

        public async Task<bool> ConsumeApi()
        {
            HttpClient _httpClient = new HttpClient();

            var hasCountries = await _context.Countries.AnyAsync();

            if (hasCountries)
            {
                return true;
            }

            //Es interesante poner la clave en un .env, pero para que sea más fácil probar el sistema, la puse en el código.
            string apiKey = "bGFqSXVpRjFVcEhzQjFoa0lLWTE2Q0l0b1BncE5jSnM5eWFiWHZncw==";
            string url = "https://api.countrystatecity.in/v1/countries";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("X-CSCAPI-KEY", apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(jsonString);
            var countries = new List<CountryModel>();

            foreach (var element in jsonDoc.RootElement.EnumerateArray())
            {
                if (element.TryGetProperty("iso2", out var iso) &&
                    element.TryGetProperty("name", out var name))
                {
                    countries.Add(new CountryModel
                    {
                        Id = 0,
                        Name = name.GetString() ?? "Desconhecido"
                    });
                }
            }

            await _context.Countries.AddRangeAsync(countries);
            await _context.SaveChangesAsync();

            return false;
        }
    }
}
