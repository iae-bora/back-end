using GeoCoordinatePortable;
using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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

            //    var id = 1;
            //var query = database.Posts    // your starting point - table in the "from" statement
            //   .Join(database.Post_Metas, // the source table of the inner join
            //      post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
            //      meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
            //      (post, meta) => new { Post = post, Meta = meta }) // selection
            //   .Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement

            //                var results = workOrders.Join(plans,
            //  wo => wo.WorkOrderNumber,
            //  p => p.WorkOrderNumber,
            //  (order, plan) => new { order.WorkOrderNumber, order.WorkDescription, plan.ScheduledDate }
            //);

            //        var query =
            //people.Join(pets,
            //            person => person,
            //            pet => pet.Owner,
            //            (person, pet) =>
            //                new { OwnerName = person.Name, Pet = pet.Name });


            return null;
        }

        private static double GetDistanceFromLatitudeAndLongitude(double lat1, double long1, double lat2, double long2)
        {
            //double latA = -31.997976f;
            //double longA = 115.762877f;
            //double latB = -31.99212f;
            //double longB = 115.763228f;

            //var locA = new GeoCoordinate(latA, longA);
            //var locB = new GeoCoordinate(latB, longB);
            //double distance = locA.GetDistanceTo(locB); // metres

            var location1 = new GeoCoordinate(lat1, long1);
            var location2 = new GeoCoordinate(lat2, long2);
            double distance = location1.GetDistanceTo(location2);

            return distance;
        }
    }
}