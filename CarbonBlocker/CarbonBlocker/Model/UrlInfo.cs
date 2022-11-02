using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonBlocker.Model
{
    public class UrlInfo
    {
        public string Url { get; set; }
        public string City { get; set; }

        public string IpAddress { get; set; }

        public string Region { get; set; }

        public Location coordinates { get; set; }

        public UrlInfo(){

            Url = "";
            City = "";
            IpAddress = "";
            Region = "";
            coordinates = new Location(0,0);
        }

    }
}
