using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using log4net;

namespace CarbonBlocker.Communications
{
    public class CarbonIntensityChecker
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(CarbonIntensityChecker));
        public static double getCarbonIntensity(string location, string time)
        {

            var client3 = new RestClient("https://carbon-aware-api.azurewebsites.net/emissions/bylocations");
            var request3 = new RestRequest();
            request3.AddQueryParameter("location", location);
            //string now = DateTime.Now.ToString("yyyy-MM-ddTHH%3Amm%3A00Z");
            //request3.AddQueryParameter("time", time);
            //request3.AddQueryParameter("location", "westeurope");
            var response3 = client3.Execute(request3);
            log.Info(response3.Content);

            double rating = 0;

            try
            {
                if(response3.Content != null & response3.Content.Length > 0)
                {
                    CarbonIntensityResponse[] carbonIntensityResponse = JsonSerializer.Deserialize<CarbonIntensityResponse[]>(response3.Content.ToString());
                    rating = carbonIntensityResponse[0].rating;
                }
                
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            


            return rating;
        }

        public static  void CheckCarbonIntensity(string ipAddress, int limit, List<string> urls)
        {
            

            
        }
    }
}
