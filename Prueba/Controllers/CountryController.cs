using Microsoft.AspNetCore.Mvc;
using Prueba.DB.Models;
using Prueba.Services.Interfaces;
using Shared;
using Shared.Dto;
namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        public readonly ICountryService _contryService;

        public CountryController(ICountryService contryService)
        {
            _contryService = contryService;
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetByFilters() => Ok(await _contryService.GetCountries());

     

    }
}
