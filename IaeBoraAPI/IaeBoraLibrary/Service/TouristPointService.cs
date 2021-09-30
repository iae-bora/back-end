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
        public static TouristPoint GetTouristPoint(Address originAddress, PlacesEnum category, List<Place> places, List<Opening_Hours> openingHours, FoodsEnum? foodPreference = null)
        {
            List<Opening_Hours> possiblePlaces;

            if (category == PlacesEnum.Restaurante)
            {
                possiblePlaces = openingHours.Where(oh => Utils.DaysOfWeekTools.TranslateDay(oh.Day_of_Week) == DateTime.Now.DayOfWeek &&
                                                          oh.Place.Restaurant_category_id == foodPreference &&
                                                          oh.Place.Category_id == category && 
                                                          oh.Open).ToList();
            }
            else
            {
                possiblePlaces = openingHours.Where(oh => Utils.DaysOfWeekTools.TranslateDay(oh.Day_of_Week) == DateTime.Now.DayOfWeek &&
                                                          oh.Open && oh.Place.Category_id == category).ToList();
            }

            if (possiblePlaces.Count == 0)
                throw new Utils.Exceptions.NotFoundPlacesException("Não há nenhum ponto turístico disponível com esses parâmetros");

            double distance = 0, auxDistance = 0;
            TouristPoint point = new();

            foreach (var possiblePlace in possiblePlaces)
            {
                auxDistance = GetDistanceFromLatitudeAndLongitude(originAddress.Latitude, originAddress.Longitude, (double)possiblePlace.Place.Latitude, (double)possiblePlace.Place.Longitude);
                // TODO: Filtrar por Horas de início e fim
                if (distance == 0 || auxDistance < distance)
                {
                    // TODO: Selecionar os com maior rating?
                    distance = auxDistance;
                    point.DistanceFromOrigin = distance;
                    point.Place = possiblePlace.Place; // Necessidade?
                    point.OpeningHours = possiblePlace;
                }
            }

            return point;
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