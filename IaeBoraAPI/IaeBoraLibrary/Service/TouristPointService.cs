using IaeBoraLibrary.Model.Enums;
using System.Collections.Generic;
using IaeBoraLibrary.Utils.Tools;
using IaeBoraLibrary.Model;
using System.Linq;
using System;

namespace IaeBoraLibrary.Service
{
    public static class TouristPointService
    {
        public static TouristPoint GetTouristPoint(Address originAddress, PlacesEnum category, List<OpeningHours> openingHours, Answer answer)
        {
            List<OpeningHours> possiblePlaces = GetOpeningPlaces(openingHours, answer.RouteDateAndTime, answer.RouteDateAndTime.AddHours(2)).
                                                                  Where(p => p.Place.Category == category).ToList();
            if (category == PlacesEnum.Restaurante)
                possiblePlaces = possiblePlaces.Where(p => p.Place.RestaurantCategory == answer.Food).ToList();

            if (possiblePlaces.Count == 0)
                return null;

            List<TouristPoint> historyPlaces = answer.TakeNewPlaces ? RouteService.GetAllRoutes(answer.User.GoogleId).Item1 : null;
            var point = PickTheTouristPoint(originAddress, answer, possiblePlaces, historyPlaces);
            point = point.OpeningHours == null ? PickTheTouristPoint(originAddress, answer, possiblePlaces) : point;

            return point;
        }

        private static TouristPoint PickTheTouristPoint(Address originAddress, 
                                                        Answer answer, 
                                                        List<OpeningHours> possiblePlaces, 
                                                        List<TouristPoint> historyPlaces = null)
        {
            double distance = 0, auxDistance = 0;
            TouristPoint point = new();
            foreach (var possiblePlace in possiblePlaces)
            {
                if (historyPlaces == null || (historyPlaces != null && !historyPlaces.Any(p => p.OpeningHours.Place.Id == possiblePlace.Place.Id)))
                {
                    auxDistance = AddressTools.GetDistanceFromLatitudeAndLongitude(originAddress.Latitude,
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
            }

            return point;
        }

        public static List<OpeningHours> GetOpeningPlaces(List<OpeningHours> openingHours, DateTime startHour, DateTime endHour)
        {
            var possiblePlaces = openingHours.Where(oh => oh.Open &&
                DaysOfWeekTools.TranslateDay(oh.DayOfWeek) == startHour.DayOfWeek &&
                oh.StartHour?.Hour <= startHour.Hour && 
                oh.EndHour?.Hour >= endHour.Hour).ToList();

            return possiblePlaces;
        }
    }
}