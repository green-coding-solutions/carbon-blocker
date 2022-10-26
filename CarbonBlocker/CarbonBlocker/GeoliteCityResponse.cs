using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonBlocker
{

    public class City
    {
        public int geoname_id { get; set; }
        public Names names { get; set; }
    }


    public class Location
    {
        public int accuracy_radius { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string time_zone { get; set; }
    }


    public class Postal
    {
        public string code { get; set; }
    }


    public class GeoliteCityResponse
    {
        public City city { get; set; }
        public Continent continent { get; set; }
        public Country country { get; set; }
        public Location location { get; set; }
        public Postal postal { get; set; }
        public RegisteredCountry registered_country { get; set; }
        public List<Subdivision> subdivisions { get; set; }
        public Traits traits { get; set; }
    }

    public class Subdivision
    {
        public string iso_code { get; set; }
        public int geoname_id { get; set; }
        public Names names { get; set; }
    }

}
