using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Context;
using System.Collections.Generic;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class RouteService
    {
        public static List<TouristPoint> CreateDetailedRoute(Answer answer)
        {
            var routeCategories = IaeBoraMLService.GetRouteCategories(answer);

            // Salva a rota no BD e pega o Id criado.

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
                places = context.Place.ToList();
                openingHours = context.Opening_Hours.ToList();
                foreach (var category in route.RouteCategories)
                {

                    var newPoint = TouristPointService.GetDetailedRoutePlace(category, places, openingHours, user);
                    newPoint.Index = count;


                    // todo: add places e oh
                    newPoint.Route = route;
                    //newPoint.Route.Id = route.Id;

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
