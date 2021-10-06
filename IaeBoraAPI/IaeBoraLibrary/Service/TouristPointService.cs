using IaeBoraLibrary.Model.Enums;
using System.Collections.Generic;
using IaeBoraLibrary.Model;
using System.Linq;
using System;

namespace IaeBoraLibrary.Service
{
    public static class TouristPointService
    {
        public static TouristPoint GetTouristPoint(Address originAddress, PlacesEnum category, List<Opening_Hours> openingHours, Answer answer)
        {
            List<Opening_Hours>  possiblePlaces = GetOpeningPlaces(openingHours, answer.RouteDateAndTime, answer.RouteDateAndTime.AddHours(2)).
                Where(p => p.Place.Category_id == category).ToList();

            if (category == PlacesEnum.Restaurante)
                possiblePlaces = possiblePlaces.Where(p => p.Place.Restaurant_category_id == answer.Food).ToList();      

            if (possiblePlaces.Count == 0)
                return null; // TODO: Add: Exceptions.NotFoundPlacesException. ?

            double distance = 0, auxDistance = 0;
            TouristPoint point = new();

            foreach (var possiblePlace in possiblePlaces)
            {
                auxDistance = Utils.AddressTools.GetDistanceFromLatitudeAndLongitude(originAddress.Latitude, 
                                                                                     originAddress.Longitude, 
                                                                                     (double)possiblePlace.Place.Latitude, 
                                                                                     (double)possiblePlace.Place.Longitude);
                if (distance == 0 || auxDistance < distance)
                {
                    distance = auxDistance;
                    point.DistanceFromOrigin = distance;
                    point.OpeningHours = possiblePlace;
                    point.StartHour = answer.RouteDateAndTime;
                    point.EndHour = answer.RouteDateAndTime.AddHours(2);
                }
            }

            return point;
        }

        public static List<Opening_Hours> GetOpeningPlaces(List<Opening_Hours> openingHours, DateTime startHour, DateTime endHour)
        {
            var possiblePlaces = openingHours.Where(oh => oh.Open &&
                Utils.DaysOfWeekTools.TranslateDay(oh.Day_of_Week) == startHour.DayOfWeek &&
                oh.Start_Hour?.Hour <= startHour.Hour && 
                oh.End_Hour?.Hour >= endHour.Hour).ToList();

            return possiblePlaces;
        }
    }
}