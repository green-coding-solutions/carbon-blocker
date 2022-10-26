using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonBlocker
{

    public class Continent
    {
        public string code { get; set; }
        public int geoname_id { get; set; }
        public Names names { get; set; }
    }

    public class Country
    {
        public bool is_in_european_union { get; set; }
        public string iso_code { get; set; }
        public int geoname_id { get; set; }
        public Names names { get; set; }
    }

    public class Names
    {
        public string es { get; set; }
        public string fr { get; set; }
        public string ja { get; set; }

        [JsonProperty("pt-BR")]
        public string PtBR { get; set; }
        public string ru { get; set; }

        [JsonProperty("zh-CN")]
        public string ZhCN { get; set; }
        public string de { get; set; }
        public string en { get; set; }
    }

    public class RegisteredCountry
    {
        public bool is_in_european_union { get; set; }
        public string iso_code { get; set; }
        public int geoname_id { get; set; }
        public Names names { get; set; }
    }

    public class GeoliteCountryResponse
    {
        public Continent continent { get; set; }
        public Country country { get; set; }
        public RegisteredCountry registered_country { get; set; }
        public Traits traits { get; set; }
    }

    public class Traits
    {
        public string ip_address { get; set; }
        public string network { get; set; }
    }
}
