using GeoCoordinatePortable;
using IaeBoraLibrary.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Linq;
using ViaCep;

namespace IaeBoraLibrary.Utils.Tools
{
    public class AddressTools
    {
        public static Address GetLatitudeAndLongitudeFromAddress(string postalCode)
        {
            var address = new ViaCepClient().Search(postalCode);
            var addressQuery = $"{address.Street} {address.Neighborhood} {address.Complement} {address.City} {address.StateInitials} {address.ZipCode}";

            var client = new RestClient(APIRoutesAndKeys.GeoCordinateAPIRoute);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("access_key", APIRoutesAndKeys.GeoCordinateAPIKey);
            request.AddQueryParameter("query", addressQuery);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Address addressToReturn = new();

                var json = JToken.Parse(response.Content);

                if (!json["data"][0].Any())
                    throw new Exceptions.AddressServiceException("Não foi possível obter o endereço do usuário.");

                addressToReturn.Latitude = (double)json["data"][0]["latitude"];
                addressToReturn.Longitude = (double)json["data"][0]["longitude"];

                return addressToReturn;
            }
            else
            {
                throw new Exceptions.AddressServiceException("Erro ao obter Latitude e Longitude do endereço do usuário. Endereço: " + postalCode);
            }
        }

        public static double GetDistanceFromLatitudeAndLongitude(double lat1, double long1, double lat2, double long2)
        {
            var location1 = new GeoCoordinate(lat1, long1);
            var location2 = new GeoCoordinate(lat2, long2);
            double distance = location1.GetDistanceTo(location2);

            return distance;
        }

        public static bool PostalCodeValidator(string postalCode)
        {
            var address = new ViaCepClient().Search(postalCode);
            return (address.ZipCode == null) ? false : true;
        }
    }
}
