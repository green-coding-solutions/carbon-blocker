using CarbonBlocker;
using CarbonBlocker.Communications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RestSharp;

using log4net;
using log4net.Config;
using System.Reflection;
using System.Security.Policy;
using System.Net;
using CarbonBlocker.Model;
using Location = CarbonBlocker.Model.Location;
using MaxMind.GeoIP2.Responses;



//TODOS:
//--1) Add CarbonAware SDK - not necessary. using api for test
//2) The application should be in system tray. popup every time check carbon intensity
//**3) Add correspondence from country and Carbon Aware locations - get latitude - longitude to region azure
//--4) Add Cabon inensity check on server url requested : done
//5) Add forecast about url adn user

//Log4net configuration
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4netconfig.config"));
var log = log4net.LogManager.GetLogger(typeof(Program));

using IHost host = Host.CreateDefaultBuilder(args).Build();

// Read configuration file

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var urls = config.GetSection("urls").Get<List<string>>();
int limit = config.GetValue<int>("limit");
int delay = config.GetValue<int>("delay");

foreach(string url in urls)
{
    log.Info($"url = {url}");
}

log.Info($"Carbon intensity limit = {limit}");
log.Info($"Check delay = {delay}");


UrlList urlList = new UrlList();

AzureRegions azureRegions = new AzureRegions();

//Check public ip address
string ipAddress = PublicIPAddressChecker.getPublicIpAddress();
log.Info("User public ip address: " + ipAddress);

//Check user info
CityResponse userCity = CountryLocator.getCity(ipAddress);
log.Info("User country:" + userCity.Country.Name);
log.Info("User city:" + userCity.City.Name);
log.Info("User location  lat:" + userCity.Location.Latitude + " - long:" + userCity.Location.Longitude);
Location userCityLocation = new Location(0, 0);
userCityLocation.Latitude = userCity.Location.Latitude != null ? userCity.Location.Latitude.Value : 0.0;
userCityLocation.Longitude = userCity.Location.Longitude != null ? userCity.Location.Longitude.Value : 0.0;
NamedGeoposition userRegionResp = azureRegions.getNearRegion(userCityLocation);
string userRegion = userRegionResp.RegionName;


log.Info("User region:" + userRegion);
if (String.IsNullOrEmpty(userRegion))
{
    userRegion = "westeurope";
    log.Info("User default region:" + userRegion);
}

//Check url public ip address
foreach (var url in urls)
{
    UrlInfo info = new UrlInfo();
    info.Url = url;

    string urlHostname = new Uri(url).Host.ToLower();
    var urlIpAddress = Dns.GetHostEntry(urlHostname);
    
    //overwrite with last ip - TODO: add all ip addresses
    foreach (var ip in urlIpAddress.AddressList)
    {
        log.Info("URL: " + info.Url);
        log.Info("URL public ip address: " + ip);
        info.IpAddress = ip.ToString();
        CityResponse urlCity = CountryLocator.getCity(ip.ToString());
        info.City = !String.IsNullOrEmpty(urlCity.Traits.AutonomousSystemOrganization)? urlCity.Traits.AutonomousSystemOrganization: urlCity.City.Name;
        log.Info("URL country:" + urlCity.Country.Name);
        log.Info("URL city:" + info.City);
        log.Info("URL location  lat:" + urlCity.Location.Latitude + " - long:" + urlCity.Location.Longitude);
        Location cityLocation = new Location(0, 0);
        cityLocation.Latitude = urlCity.Location.Latitude != null ? urlCity.Location.Latitude.Value : 0.0;
        cityLocation.Longitude = urlCity.Location.Longitude != null ? urlCity.Location.Longitude.Value : 0.0;
        NamedGeoposition urlRegionResp =  azureRegions.getNearRegion(cityLocation);
        info.Region = urlRegionResp.RegionName;
        log.Info("URL region:" + info.Region);
        if (String.IsNullOrEmpty(info.Region))
        {
            info.Region = "westeurope";
            log.Info("URL default region:" + info.Region);
        }
        
    }

    urlList.Add(info);


}


/*
//call carbon aware sdk and change etc/hosts every $delay seconds

var startTimeSpan = TimeSpan.Zero;
var periodTimeSpan = TimeSpan.FromSeconds(delay);

var timer = new System.Threading.Timer((e) =>
{
*/
    log.Info("User IP:" + ipAddress);

    double carbonIntensityUser = CarbonIntensityChecker.getCarbonIntensity(userRegion, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:00Z"));

    List<string> urlsToBlock = new List<string>();

    foreach (UrlInfo info in urlList.GetList())
    {
        log.Info("URL:" + info.Url);
        log.Info("URL IP:" + info.IpAddress);
        log.Info("URL City:" + info.City);
        log.Info("URL Region:" + info.Region);
        double carbonIntensityUrl = CarbonIntensityChecker.getCarbonIntensity(info.Region, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:00Z"));

        if ((carbonIntensityUser + carbonIntensityUrl) > limit)
        {
        urlsToBlock.Add(info.Url);

        }

        log.Info("----------------------------------------------------------------------------------------------");
        log.Info("Limit:" + limit + " :: " + "User CI:" + carbonIntensityUser + " - Url CI:" + carbonIntensityUrl);
        log.Info("----------------------------------------------------------------------------------------------");
    }    
    

        HostsFileManager.changeHostsFile(urlsToBlock);


//}, null, startTimeSpan, periodTimeSpan);




Console.ReadLine();
