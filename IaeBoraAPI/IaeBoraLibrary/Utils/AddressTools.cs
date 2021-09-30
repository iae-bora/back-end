using IaeBoraLibrary.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using ViaCep;

namespace IaeBoraLibrary.Utils
{
    public class AddressTools
    {
        public static Address GetLatitudeAndLongitudeFromAddress(string CEP)
        {
            var address = new ViaCepClient().Search(CEP);
            var addressQuery = $"{address.Street} {address.Neighborhood} {address.Complement} {address.City} {address.StateInitials} {address.ZipCode}";

            var client = new RestClient(APIRoutesAndKeys.GeoCordinateAPIRoute);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("access_key", APIRoutesAndKeys.GeoCordinateAPIKey);
            request.AddQueryParameter("query", addressQuery);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Address addressToReturn = new Address();

                var json = JToken.Parse(response.Content);

                addressToReturn.Latitude = (double)json["data"][0]["latitude"];
                addressToReturn.Longitude = (double)json["data"][0]["longitude"];

                return addressToReturn;
            }
            else
            {
                // TODO: Arrumar
                throw new System.Exception("a");
            }
        }
    }
}
