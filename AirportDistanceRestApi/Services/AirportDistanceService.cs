using AirportDistanceRestApi.Entities;
using AirportDistanceRestApi.Extensions;
using AirportDistanceRestApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AirportDistanceRestApi.Services
{
    public class AirportDistanceService : IAirportDistanceService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public AirportDistanceService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<double> Calculate(IataCode iataCode)
        {
            var cacheKey = $"{iataCode.FromAirport}-{iataCode.ToAirport}";
            var cacheKeyReverse = $"{iataCode.ToAirport} - {iataCode.FromAirport}";

            //check value in cache
            if (!_memoryCache.TryGetValue<double>(cacheKey, out double result))
            {
                var httpClient = _clientFactory.CreateClient();

                var fromReqTask = httpClient.GetAsync($"http://iatageo.com/getLatLng/{iataCode.FromAirport}");
                var toReqTask = httpClient.GetAsync($"http://iatageo.com/getLatLng/{iataCode.ToAirport}");

                await Task.WhenAll(fromReqTask, toReqTask);

                var fromReq = fromReqTask.Result;
                var toReq = toReqTask.Result;

                if (!toReq.IsSuccessStatusCode || !fromReq.IsSuccessStatusCode)
                {
                    return -1;
                }

                //parse results

                var from = JsonSerializer.Deserialize<AirportResponse>(
                    await fromReq.Content.ReadAsStringAsync()
                    );

                var to = JsonSerializer.Deserialize<AirportResponse>(
                    await toReq.Content.ReadAsStringAsync()
                    );

                result = from.GetDistance(to);

                _memoryCache.Set<double>(cacheKey, result);
                _memoryCache.Set<double>(cacheKeyReverse, result);
            }
            return result;
        }
    }
}
