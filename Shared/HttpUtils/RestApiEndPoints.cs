using Shared;

namespace Shared.HttpUtils
{ 
public class RestApiEndPoints
    {
        private readonly HostService _hostService;

        public RestApiEndPoints(HostService hostService)
        { _hostService = hostService; }

        public string BaseAddress => _hostService.Host;

        public string CarEndPoint => $"https://localhost:7202/api/Car";
        public string CountryEndPoint => $"https://localhost:7202/api/Country";

    }
}