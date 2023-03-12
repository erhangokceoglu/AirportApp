using AirportDistanceRestApi.Entities;

namespace AirportDistanceRestApi.Interfaces
{
    public interface IAirportDistanceService
    {
        Task<double> Calculate(IataCode iataCode);
    }
}
