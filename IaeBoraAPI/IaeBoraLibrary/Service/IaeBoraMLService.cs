using System.Collections.Generic;
using IaeBoraLibrary.Model.Enums;
using IaeBoraLibrary.Model;
using IaeBoraLibrary.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;

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
            var client = new RestClient(APIRoutes.MachineLearningURL);
            var request = new RestRequest(APIRoutes.MachineLearningCreateRouteEndPoint, Method.POST);
            request.AddXmlBody(answers);
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
