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
using CarbonBlocker.Data;


//TODOS:
//--1) Add CarbonAware SDK - not necessary. using api for test
//2) The application should be in system tray. popup every time check carbon intensity
//3) Add correspondence from country and Carbon Aware locations
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

//Check public ip address
string ipAddress = PublicIPAddressChecker.getPublicIpAddress();
log.Info("User public ip address: " + ipAddress);

//Check country
string country = CountryLocator.getCountry(ipAddress);
log.Info("User country: " + country);

//Check url public ip address
foreach(var url in urls)
{
    UrlInfo info = new UrlInfo();
    info.Url = url;

    string urlHostname = new Uri(url).Host.ToLower();
    var urlIpAddress = Dns.GetHostEntry(urlHostname);
    
    //overwrite with last ip - TODO: add all ip addresses
    foreach (var ip in urlIpAddress.AddressList)
    {
        log.Info("URL public ip address: " + ip);
        info.IpAddress = ip.ToString();
        string urlCountry = CountryLocator.getCountry(ip.ToString());
        info.Country = country;
        log.Info("URL country: " + country);
    }

    urlList.Add(info);


}


//call carbon aware sdk and change etc/hosts every $delay seconds

var startTimeSpan = TimeSpan.Zero;
var periodTimeSpan = TimeSpan.FromSeconds(delay);

var timer = new System.Threading.Timer((e) =>
{

    log.Info("User IP:" + ipAddress);

    double carbonIntensityUser = CarbonIntensityChecker.getCarbonIntensity("westeurope", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:00Z"));

    List<string> urls = new List<string>();

    foreach (UrlInfo info in urlList.GetList())
    {
        log.Info("URL IP:" + info.IpAddress);
        double carbonIntensityUrl = CarbonIntensityChecker.getCarbonIntensity("westeurope", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:00Z"));

        if ((carbonIntensityUser + carbonIntensityUrl) > limit)
        {
            urls.Add(info.Url);

        }

        log.Info("----------------------------------------------------------------------------------------------");
        log.Info("Limit:" + limit + " :: " + "User CI:" + carbonIntensityUser + " - Url CI:" + carbonIntensityUrl);
        log.Info("----------------------------------------------------------------------------------------------");
    }    
    

        HostsFileManager.changeHostsFile(urls);


}, null, startTimeSpan, periodTimeSpan);




Console.ReadLine();
