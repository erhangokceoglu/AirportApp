
using AirportDistanceRestApi.Entities;
using GeoCoordinatePortable;
using System;

namespace AirportDistanceRestApi.Extensions
{
    public static class DistanceExtensions
    {
        public static double GetDistance(this AirportResponse fromAirport, AirportResponse toAirport)
        {
            var rFromLatitude = Math.PI * fromAirport.LatitudeDouble / 180;
            var rToLatitude = Math.PI * toAirport.LatitudeDouble / 180;
            var theta = fromAirport.LongitudeDouble - toAirport.LongitudeDouble;
            var rTheta = Math.PI * theta / 180;

            var distInMiles =
                Math.Sin(rFromLatitude) * Math.Sin(rToLatitude) + Math.Cos(rFromLatitude) *
                Math.Cos(rToLatitude) * Math.Cos(rTheta);

            distInMiles = Math.Acos(distInMiles);
            distInMiles = distInMiles * 180 / Math.PI;
            distInMiles = distInMiles * 60 * 1.1515;
            return distInMiles;
        }
    }
}
