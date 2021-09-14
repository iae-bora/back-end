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
            return CreateRouteCategories(SendAnswer(userAnswers), userAnswers.User);
        }

        private static List<PlacesEnum> SendAnswer(Answer answers)
        {
            var client = new RestClient(APIRoutes.MachineLearningURL);
            var request = new RestRequest("", Method.POST); // Colocar o endpoint (se tiver)
            request.AddXmlBody(answers);
            var response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<PlacesEnum>>(response.Content);
            else
                throw new Utils.Exceptions.MLServiceException("Não foi possível obter as Rotas originadas pelo serviço de Machine Learning. ML API: " + response.ErrorMessage);
        }

        private static Route CreateRouteCategories(List<PlacesEnum> placesCategories, User user)
        {
            Route routeCategories = new(placesCategories)
            {
                User = user,
                RouteDate = DateTime.Now
            };

            return routeCategories;
        }
    }
}
