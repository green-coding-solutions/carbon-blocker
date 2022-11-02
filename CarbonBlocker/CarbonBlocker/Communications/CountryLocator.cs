
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using log4net.Core;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using CarbonBlocker.Model;

namespace CarbonBlocker.Communications
{
    public class CountryLocator
    {
        private static string code = "mGZJSIS1I9pS1GyT";
        private static ILog log = log4net.LogManager.GetLogger(typeof(CountryLocator));
        public static string getCountry(string ipAddress)
        {


            var client = new RestClient("https://geolite.info/geoip/v2.1/country/" + ipAddress);
            var request = new RestRequest();
            request.AddHeader("Authorization", "Basic NzgyMzEyOm1HWkpTSVMxSTlwUzFHeVQ=");
            var response = client.Execute(request);

            log.Info("Data:" + response.Content.ToString());

            GeoliteCountryResponse? geoliteResponse =
                           JsonSerializer.Deserialize<GeoliteCountryResponse>(response.Content.ToString());

            log.Info("Country:" + geoliteResponse.registered_country.iso_code);

            return geoliteResponse.registered_country.iso_code;
        }

        public static CityResponse getCity(string ipAddress){

            string ret = "";
            var client = new WebServiceClient(782312, code, host: "geolite.info");
            CityResponse resp = client.City(ipAddress);
            
            return resp;
        }

        public static CarbonBlocker.Model.Location getLatitudeLong(string ipAddress)
        {

            var client = new WebServiceClient(782312, code, host: "geolite.info");
            CityResponse resp = client.City(ipAddress);
            CarbonBlocker.Model.Location cityLocation = new CarbonBlocker.Model.Location(0,0);
            cityLocation.Latitude = resp.Location.Latitude != null ? resp.Location.Latitude.Value : 0.0;
            cityLocation.Longitude = resp.Location.Longitude != null ? resp.Location.Longitude.Value : 0.0;
            return cityLocation;
        }


    }


}
