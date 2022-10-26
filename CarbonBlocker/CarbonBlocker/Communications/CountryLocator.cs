
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

namespace CarbonBlocker.Communications
{
    public class CountryLocator
    {
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

            log.Info("Country:" + geoliteResponse.country.iso_code);

            return geoliteResponse.country.iso_code;
        }
    }
}
