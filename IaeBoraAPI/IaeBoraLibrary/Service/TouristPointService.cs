using IaeBoraLibrary.Model.Enums;
using System.Collections.Generic;
using GeoCoordinatePortable;
using IaeBoraLibrary.Model;
using System.Linq;
using System;

namespace IaeBoraLibrary.Service
{
    public static class TouristPointService
    {
        public static TouristPoint GetDetailedRoutePlace(PlacesEnum category, List<Place> places, List<Opening_Hours> openingHours, User user)
        {
            double lat = -23.718412, lon = -46.537121; // Test

            var allowedHours = openingHours.Where(oh => Utils.DaysOfWeekTools.TranslateDay(oh.Day_of_Week) == DateTime.Now.DayOfWeek && oh.Open);
            var possiblePlaces = places.Where(p => p.Category_id == category);

            var query = possiblePlaces
                .Join(allowedHours,
                    place => place.Id,
                    hour => hour.Id,
                    (place, hour) => new { Place = place, Hour = hour })
                .Where(placeAndHour => placeAndHour.Place.Id == placeAndHour.Hour.Id).ToList();

            foreach (var placeAndHour in query)
            {
                var distance = GetDistanceFromLatitudeAndLongitude(lat, lon, (double)placeAndHour.Place.Latitude, (double)placeAndHour.Place.Longitude);
            }

            return null;
        }

        private static double GetDistanceFromLatitudeAndLongitude(double lat1, double long1, double lat2, double long2)
        {
            var location1 = new GeoCoordinate(lat1, long1);
            var location2 = new GeoCoordinate(lat2, long2);
            double distance = location1.GetDistanceTo(location2);

            return distance;
        }
    }
}