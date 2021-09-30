using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class RouteService
    {
        public static List<TouristPoint> CreateDetailedRoute(Answer answer)
        {
            var routeCategories = IaeBoraMLService.GetRouteCategories(answer);
            return SaveRoutesDetails(routeCategories, answer.User);
        }

        public static List<TouristPoint> SaveRoutesDetails(Route route, User user)
        {
            List<Place> places;
            List<Opening_Hours> openingHours;
            List<TouristPoint> touristPoints = new List<TouristPoint>();
            int count = 0;

            using (var context = new Context())
            {
                context.Routes.Add(route);
                context.SaveChanges();

                places = context.Place.ToList();
                openingHours = context.Opening_Hours.ToList();

                foreach (var category in route.RouteCategories)
                {
                    var newPoint = TouristPointService.GetTouristPoint(category, places, openingHours, user);
                    newPoint.Index = count;
                    newPoint.Route = route;

                    context.Entry(newPoint.Place).State = EntityState.Unchanged;
                    context.Entry(newPoint.OpeningHours).State = EntityState.Unchanged;

                    context.TouristPoints.Add(newPoint);
                    touristPoints.Add(newPoint);
                    count++;
                }
                context.SaveChanges();
            }

            return touristPoints;
        }
    }
}
