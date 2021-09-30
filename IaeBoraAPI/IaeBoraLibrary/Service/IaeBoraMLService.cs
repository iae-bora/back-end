using System.Collections.Generic;
using IaeBoraLibrary.Model.Enums;
using IaeBoraLibrary.Model;
using IaeBoraLibrary.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using Newtonsoft.Json.Serialization;

namespace IaeBoraLibrary.Service
{
    public static class IaeBoraMLService
    {
        public static Route GetRouteCategories(Answer userAnswers)
        { 
            return CreateRouteCategories(SendAnswer(userAnswers), userAnswers);
        }

        private static List<PlacesEnum> SendAnswer(Answer answers)
        {
            var client = new RestClient(APIRoutesAndKeys.MachineLearningRoute);
            var request = new RestRequest(APIRoutesAndKeys.MachineLearningEndPointRoute, Method.POST);

            request.AddHeader("Content-Type", "application/json");

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(answers, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });


            //var json = JsonConvert.SerializeObject(answers, Formatting.Indented);
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
                FoodPreference = answer.Food,
                RouteDate = DateTime.Now
            };

            return routeCategories;
        }
    }
}
