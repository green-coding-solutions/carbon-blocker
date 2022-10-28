using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonBlocker.Data
{
    public class UrlInfo
    {
        public string Url { get; set; }
        public string Country { get; set; }

        public string IpAddress { get; set; }

        public string Location { get; set; }

        public UrlInfo(){

            Url = "";
            Country = "";
            IpAddress = "";
            Location = "";
        }

    }
}
