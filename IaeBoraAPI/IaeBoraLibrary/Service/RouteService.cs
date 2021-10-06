using Microsoft.EntityFrameworkCore;
using IaeBoraLibrary.Model.Context;
using System.Collections.Generic;
using IaeBoraLibrary.Model;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class RouteService
    {
        public static List<TouristPoint> CreateDetailedRoute(Answer answer)
        {
            var routeCategories = IaeBoraMLService.GetRouteCategories(answer);
            return CreateAndSaveRoutesDetails(routeCategories, answer);
        }

        public static List<TouristPoint> CreateAndSaveRoutesDetails(Route route, Answer answer)
        {
            List<Opening_Hours> openingHours;
            List<TouristPoint> touristPoints = new();
            int count = 0;

            using (var context = new Context())
            {
                context.Routes.Add(route);
                context.Entry(route.User).State = EntityState.Unchanged;
                context.SaveChanges();

                openingHours = context.Opening_Hours.Include("Place").ToList();

                var address = Utils.AddressTools.GetLatitudeAndLongitudeFromAddress(route.User.Address);

                foreach (var category in route.RouteCategories)
                {
                    var newPoint = TouristPointService.GetTouristPoint(address, category, openingHours, answer);
                    if (newPoint != null)
                    {
                        answer.RouteDateAndTime = newPoint.EndHour;

                        newPoint.Index = count;
                        newPoint.Route = route;

                        context.Entry(newPoint.OpeningHours).State = EntityState.Unchanged;

                        context.TouristPoints.Add(newPoint);
                        touristPoints.Add(newPoint);

                        address.Latitude = (double)newPoint.OpeningHours.Place.Latitude;
                        address.Longitude = (double)newPoint.OpeningHours.Place.Longitude;

                        count++;
                        if (count == answer.PlacesCount) 
                            break; //TODO: Remover futuramente com a nova task.
                    }
                }
                context.SaveChanges();
            }

            return touristPoints;
        }
    }
}
