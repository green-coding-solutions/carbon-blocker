using CarbonBlocker;
using CarbonBlocker.Communications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RestSharp;

using log4net;
using log4net.Config;
using System.Reflection;


//TODOS:
//1) Add CarbonAware SDK
//2) The application should be in system tray. popup every time check carbon intensity
//3) Add correspondence from country and Carbon Aware locations
//4) Add Cabon inensity check on server url requested

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

log.Info($"url = {urls}");
log.Info($"Carbon intensity limit = {limit}");
log.Info($"Check delay = {delay}");




//Check public ip address
string ipAddress = PublicIPAddressChecker.getPublicIpAddress();
log.Info("User public ip address: " + ipAddress);

//Check country
string country = CountryLocator.getCountry(ipAddress);
log.Info("User country: " + country);


//call carbon aware sdk and change etc/hosts every $delay seconds

var startTimeSpan = TimeSpan.Zero;
var periodTimeSpan = TimeSpan.FromSeconds(delay);

var timer = new System.Threading.Timer((e) =>
{

    System.Console.WriteLine("IP:" + ipAddress);


    double carbonIntensity = CarbonIntensityChecker.getCarbonIntensity("westeurope", "2022-03-01T15:30:00Z");
    log.Info("----------------------------------------------------------");
    log.Info("Limit:" + limit + " :: " + "Carbon Intensity:" + carbonIntensity);
    log.Info("----------------------------------------------------------");

    if (carbonIntensity > limit)
    {
        HostsFileManager.changeHostsFile(urls);

    }
}, null, startTimeSpan, periodTimeSpan);




Console.ReadLine();
