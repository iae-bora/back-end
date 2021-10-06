using Microsoft.EntityFrameworkCore;
using IaeBoraLibrary.Model.Context;
using System.Collections.Generic;
using IaeBoraLibrary.Model;
using System.Linq;
using RestSharp;

namespace IaeBoraLibrary.Service
{
    public static class RouteService
    {
        public static List<Route> GetAllRoutes(string userId)
        {
            using (var context = new Context())
            {
                var routes = context.Routes.Where(r => r.User.GoogleId == userId).OrderByDescending(r => r.RouteDate).ToList();
                if (routes == null)
                    throw new Utils.Exceptions.NullPlacesFoundException("Esse usuário não possui nenhuma rota cadastrada.");


                // ================     Bagulho Feio    =====================
                dynamic route = new JsonObject();
                route.name = "Rota de Teste";

                dynamic points = new JsonArray();

                dynamic point1 = new JsonObject();
                point1.name = "Teste 1";

                dynamic point2 = new JsonObject();
                point2.name = "Teste 2";

                points.Add(point1);
                points.Add(point2);

                route.points = points;
                // ==========================================================


                return routes;
            }
        }

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
