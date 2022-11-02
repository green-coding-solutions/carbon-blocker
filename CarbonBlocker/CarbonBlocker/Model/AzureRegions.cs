using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarbonBlocker.Model
{
    public class AzureRegions
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        private Dictionary<String, NamedGeoposition> regionGeopositionMapping = new Dictionary<string, NamedGeoposition>();
        public AzureRegions()
        {
            var path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName, "");
            var data = File.ReadAllText(path + "azure-regions.json");
            List<NamedGeoposition> regionList = JsonSerializer.Deserialize<List<NamedGeoposition>>(data, options) ?? new List<NamedGeoposition>();
            foreach (NamedGeoposition region in regionList)
            {
                regionGeopositionMapping.Add(region.RegionName, region);
            }
        }


        public NamedGeoposition getNearRegion(Location location)
        {
            NamedGeoposition region = new NamedGeoposition();
            double distance = 15000000;
            foreach(NamedGeoposition regionTmp in regionGeopositionMapping.Select(r=>r.Value))
            {
                if(regionTmp.Latitude == null)
                    continue;
                if(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                {
                    regionTmp.Latitude = regionTmp.Latitude.Replace(".", ",");
                    regionTmp.Longitude = regionTmp.Longitude.Replace(".", ",");
                }

                Location regionLocation = new Location(Double.Parse(regionTmp.Latitude), Double.Parse(regionTmp.Longitude));
                double distanceTmp = CalculateDistance(location, regionLocation);
                if (distanceTmp < distance)
                {
                    region = regionTmp;
                    distance = distanceTmp;
                }
            }
            return region;
        }

        public double CalculateDistance(Location point1, Location point2)
        {
            var d1 = point1.Latitude * (Math.PI / 180.0);
            var num1 = point1.Longitude * (Math.PI / 180.0);
            var d2 = point2.Latitude * (Math.PI / 180.0);
            var num2 = point2.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

    }


}
