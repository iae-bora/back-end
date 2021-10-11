using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Context;
using IaeBoraLibrary.Model.Enums;
using IaeBoraLibrary.Utils;
using IaeBoraLibrary.Utils.Tools;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class IaeBoraMLService
    {
        public static Route GetRouteCategories(Answer answer)
        { 
            return CreateRouteCategories(SendAnswer(answer), answer);
        }

        private static List<PlacesEnum> SendAnswer(Answer answer)
        {
            var client = new RestClient(APIRoutesAndKeys.MachineLearningRoute);
            var request = new RestRequest(APIRoutesAndKeys.MachineLearningEndPointRoute, Method.POST);

            var placesCategory = GetOpenCategories(answer);
            var newAnswer = DynamicTools.ToDynamic(answer);
            newAnswer.places = placesCategory;

            request.AddHeader("Content-Type", "application/json");
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(newAnswer, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<PlacesEnum>>(response.Content);
            else
                throw new Utils.Exceptions.MLServiceException("Não foi possível obter as Rotas originadas pelo serviço de Machine Learning. ML API: " + response.ErrorMessage);
        }

        private static Route CreateRouteCategories(List<PlacesEnum> placesCategories, Answer answer)
        {
            Route routeCategories = new(placesCategories)
            {
                User = answer.User,
                RouteDate = answer.RouteDateAndTime
            };

            return routeCategories;
        }

        private static List<PlacesEnum> GetOpenCategories(Answer answer)
        {
            List<OpeningHours> openingPlaces;
            using (var context = new Context())
            {
                openingPlaces = context.OpeningHours.Include("Place").ToList();
            }

            var hoursGrouped = TouristPointService.GetOpeningPlaces(
                openingPlaces,
                answer.RouteDateAndTime,
                answer.RouteDateAndTime.AddHours(answer.PlacesCount * 2)).
                GroupBy(p => p.Place.Category).ToList();

            var placesCategory = new List<PlacesEnum>();

            foreach (var openingHour in hoursGrouped)
                placesCategory.Add(openingHour.Key);

            return placesCategory;
        }
    }
}
