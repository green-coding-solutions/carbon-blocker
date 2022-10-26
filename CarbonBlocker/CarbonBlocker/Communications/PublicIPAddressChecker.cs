using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Text.Json;

namespace CarbonBlocker.Communications
{
    public class PublicIPAddressChecker
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(PublicIPAddressChecker));
        public static string getPublicIpAddress()
        {
            //Check my IP
            var client = new RestClient("https://api.ipify.org");
            var response = client.Execute(new RestRequest());
            var ipAddress = response.Content;

            log.Info("ipAddress:" + ipAddress);
            return ipAddress == null ? "" : ipAddress;
        }
    }
}
