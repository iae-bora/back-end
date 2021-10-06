using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using IaeBoraLibrary.Model.Context;
using System.Collections.Generic;
using IaeBoraLibrary.Model;
using Newtonsoft.Json;
using System.Linq;
using RestSharp;

namespace IaeBoraLibrary.Service
{
    public static class RouteService
    {
        public static string GetLastRouteJson(string userId)
        {
            var routeAndPoint = GetAllRoutes(userId);
            return GetFormattedRouteJson(new List<Route> { routeAndPoint.Item2.OrderByDescending(r => r.RouteDate).First() }, routeAndPoint.Item1);
        }

        public static string GetAllRoutesJson(string userId)
        {
            var routeAndPoint = GetAllRoutes(userId);
            return GetFormattedRouteJson(routeAndPoint.Item2, routeAndPoint.Item1);
        }

        public static (List<TouristPoint>, List<Route>) GetAllRoutes(string userId)
        {
            List<TouristPoint> points;
            List<Route> routes;

            using (var context = new Context())
            {
                points = context.TouristPoints.Include("OpeningHours").Include("OpeningHours.Place").Include("Route").Where(p => p.Route.User.GoogleId == userId).ToList();
                routes = context.Routes.Where(r => r.User.GoogleId == userId).OrderByDescending(r => r.RouteDate).ToList();
            }

            if (routes.Count == 0)
                throw new Utils.Exceptions.NullPlacesFoundException("Esse usuário não possui nenhuma rota cadastrada.");

            return (points, routes);
        }

        public static string GetFormattedRouteJson(List<Route> routes, List<TouristPoint> allTouristPoints)
        {
            dynamic routesJson = new JsonArray();
            foreach (var route in routes)
            {
                dynamic routeJson = new JsonObject();

                routeJson.id = route.Id;
                routeJson.routeDate = route.RouteDate;

                dynamic pointsJson = new JsonArray();

                foreach (var touristPoint in allTouristPoints.Where(p => p.Route.Id == route.Id))
                    pointsJson.Add(touristPoint);

                routeJson.touristPoints = pointsJson;
                routesJson.Add(routeJson);
            }

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(routesJson, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver,
            });

            return json;
        }

        public static string CreateDetailedRoute(Answer answer)
        {
            var routeCategories = IaeBoraMLService.GetRouteCategories(answer);
            return GetFormattedRouteJson(new List<Route> { routeCategories }, CreateAndSaveDetailedRoute(routeCategories, answer));
        }

        public static List<TouristPoint> CreateAndSaveDetailedRoute(Route route, Answer answer)
        {
            List<OpeningHours> openingHours;
            List<TouristPoint> touristPoints = new();
            int count = 0;

            using (var context = new Context())
            {
                context.Routes.Add(route);
                context.Entry(route.User).State = EntityState.Unchanged;
                context.SaveChanges();

                openingHours = context.OpeningHours.Include("Place").ToList();

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
