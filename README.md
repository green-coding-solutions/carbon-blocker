## Description

With Carbon Blocker we have create a tool that saves over-intensive carbon emissions
by blocking network DNS resolution when the carbon intensity is high.

It does this by hooking into standard mechanisms on the operating system, 
namely the *hosts* on Windows and Unix like systems.
The file is only modified when the CarbonAwareSDK from the Green Software Foundation
delivers a grid intensity higher than 50 gCO2e/kWh.

Carbon Blocker stands out by not only being targeted to software engineers or
DevOps, but also to regular users who would like to be in the driver seat
and take action in saving carbon.

The logic of Carbon Blocker is very similar to renonwned adblockers like 
Adblock Plus which have been around for a long time.

Users can easily see which hostnames are blocked in the txt like file and also
add and remove hostnames they do not want to be accessed when their grid is 
currently running on coal.

Our tool runs on all major platforms (Linux, macOS and Windows) and 
has an enormous applicability as it can basically block all network operations 
from any app or website that the user does not want to make requests in carbon intesive times.

This makes it extremely general and saves developers from creating custom solutions
for every app or software out there.

The impact of Carbon Blocker can very quickly add up when we think about adding
software update URLs from for instance big apps like Firefox or VSCode, Steam Game Downloads
( eg. Call of Duty for instance runs in the GB range),
and also streaming hosts from popular social media sites, which the user only wants to use 
in a conscious way when it does not really harm the environment.

The roadmap for carbon blocker is to be leveled up with a nice GUI that 
serves as a transparent proxy so that the user can select for every request individually
if this has to run in carbon intensive times.
Furthermore the used blocklist shall be given to the community in the same way 
that popular blocking lists for Adblock Plus have been developed and maintained
by the community so that it generates the most value and impact.

So this is our short summary. We hope you like the project as much as we do :)

## Youtube Pitch

See our pitch on Youtube for a short intro: https://www.youtube.com/watch?v=U3Ry-VitNOI

## Supported Platforms

CarbonBlocker runs on all major platforms:
- Linux
- macOS
- Windows

## How does it work?

CarbonBlocker can block the network DNS resolution through modifying the *hosts* file 
of the operating system.

It comes with a sample set of hostnames that highlight the concept but is designed to use any list 
of hostnames that is tailored to the users needs.

It uses the [Carbon Aware SDK](about:blank) from the [Green Software Foundation](https://greensoftware.foundation/)
to determine the current grid intensity of the source location (Download Server) and
the target location (User). 
If any of these locations has a higher grid intensity then **50 gCO2e/kWh** then
the specific hostname will be added to the *hosts* file and thus the name resolution
will fail on the next request.

The hostnames to be blocked are appended if the grid intensity is high and also 
removed if the grid intensity is low.

## Building

When you clone the repository you can either build from source through
the well known `dotnet build` command.
Keep in mind though that the default build target is Windows.

You have to either change the build target to you specific platform or modify
the *hosts_file_path* in the `appsettings.json` file so it finds the 
*hosts* file on your OS.

The alternative is to just use the precompiled packages under */CarbonBlocker/CarbonBlocker/deploy/*
and then the sub-folder for your platform.

## Use

On Windows just open a *cmd* window with administrator privileges and run the 
*CarbonBlocker.exe*.

On macOS and Linux open a terminal and run `sudo ./CarbonBlocker`.

The tool will then parse the current hostname list, find out their current carbon
intensity and also the carbon intensity of the user location.

The pre-defined hostnames will then be added to the *hosts* file.

## Sample output

```
2022-11-04 15:55:04,827 INFO Program.<Main>$[50] - url = https://download.cdnet.com/
2022-11-04 15:55:05,069 INFO Program.<Main>$[50] - url = https://adobe-premiere.en.softonic.com
2022-11-04 15:55:05,073 INFO Program.<Main>$[50] - url = https://youtubedownload.minitool.com
2022-11-04 15:55:05,073 INFO Program.<Main>$[55] - Carbon intensity limit = 50
2022-11-04 15:55:05,073 INFO Program.<Main>$[56] - Check delay = 60
2022-11-04 15:55:10,826 INFO CarbonBlocker.Communications.PublicIPAddressChecker.getPublicIpAddress[24] - ipAddress:79.225.179.43
2022-11-04 15:55:10,830 INFO Program.<Main>$[61] - User public ip address: 79.225.179.43
2022-11-04 15:55:11,127 INFO Program.<Main>$[61] - User country:Germany
2022-11-04 15:55:11,129 INFO Program.<Main>$[61] - User city:Berlin
2022-11-04 15:55:11,132 INFO Program.<Main>$[61] - User location  lat:52.5466 - long:13.4415
2022-11-04 15:55:11,136 INFO Program.<Main>$[75] - User region:germanynorth
2022-11-04 15:55:12,096 INFO Program.?[?] - URL: https://adobe-premiere.en.softonic.com
2022-11-04 15:55:12,098 INFO Program.?[?] - URL public ip address: 35.227.233.104
2022-11-04 15:55:12,309 INFO Program.?[?] - URL country:United States
2022-11-04 15:55:12,309 INFO Program.?[?] - URL city:GOOGLE
2022-11-04 15:55:12,310 INFO Program.?[?] - URL location  lat:39.1027 - long:-94.5778
2022-11-04 15:55:12,310 INFO Program.?[?] - URL region:centralus
2022-11-04 15:55:13,931 INFO Program.?[?] - URL: https://download.cnet.com
2022-11-04 15:55:13,931 INFO Program.?[?] - URL public ip address: 2a04:4e42:4c::666
2022-11-04 15:55:14,155 INFO Program.?[?] - URL country:United States
2022-11-04 15:55:14,156 INFO Program.?[?] - URL city:FASTLY
2022-11-04 15:55:14,156 INFO Program.?[?] - URL location  lat:37.751 - long:-97.822
2022-11-04 15:55:14,156 INFO Program.?[?] - URL region:centralus

2022-11-04 15:55:27,185 INFO CarbonBlocker.Communications.CarbonIntensityChecker.?[?] -
2022-11-04 15:55:27,186 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:27,187 INFO Program.?[?] - Limit:50 :: User CI:0 - Url CI:0
2022-11-04 15:55:27,188 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:27,188 INFO Program.?[?] - URL:https://adobe-premiere.en.softonic.com
2022-11-04 15:55:27,188 INFO Program.?[?] - URL IP:35.227.233.104
2022-11-04 15:55:27,189 INFO Program.?[?] - URL City:GOOGLE
2022-11-04 15:55:27,189 INFO Program.?[?] - URL Region:centralus
2022-11-04 15:55:27,943 INFO CarbonBlocker.Communications.CarbonIntensityChecker.?[?] - [{"location":"MISO_MASON_CITY","time":"2022-11-04T14:55:00+00:00","rating":697.62506506,"duration":"00:05:00"}]
2022-11-04 15:55:32,902 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:32,902 INFO Program.?[?] - Limit:50 :: User CI:0 - Url CI:697.62506506
2022-11-04 15:55:32,902 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:32,902 INFO Program.?[?] - URL:https://download.cnet.com
2022-11-04 15:55:32,902 INFO Program.?[?] - URL IP:199.232.198.154
2022-11-04 15:55:32,902 INFO Program.?[?] - URL City:FASTLY
2022-11-04 15:55:32,902 INFO Program.?[?] - URL Region:centralus
2022-11-04 15:55:33,419 INFO CarbonBlocker.Communications.CarbonIntensityChecker.?[?] - [{"location":"MISO_MASON_CITY","time":"2022-11-04T14:55:00+00:00","rating":697.62506506,"duration":"00:05:00"}]
2022-11-04 15:55:48,052 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:48,052 INFO Program.?[?] - URL:https://youtubedownload.minitool.com
2022-11-04 15:55:48,052 INFO Program.?[?] - URL IP:104.18.21.178
2022-11-04 15:55:48,053 INFO Program.?[?] - URL City:CLOUDFLARENET
2022-11-04 15:55:48,053 INFO Program.?[?] - URL Region:francesouth
2022-11-04 15:55:48,526 INFO CarbonBlocker.Communications.CarbonIntensityChecker.?[?] -
2022-11-04 15:55:48,527 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:48,527 INFO Program.?[?] - Limit:50 :: User CI:0 - Url CI:0
2022-11-04 15:55:48,527 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:48,053 INFO Program.?[?] - URL Region:francesouth
2022-11-04 15:55:48,526 INFO CarbonBlocker.Communications.CarbonIntensityChecker.?[?] -
2022-11-04 15:55:48,527 INFO Program.?[?] - ----------------------------------------------------------------------------------------------
2022-11-04 15:55:48,527 INFO Program.?[?] - Limit:50 :: User CI:0 - Url CI:0
2022-11-04 15:55:48,527 INFO CarbonBlocker.Communications.HostsFileManager.?[?] - ************
2022-11-04 15:55:48,528 INFO CarbonBlocker.Communications.HostsFileManager.?[?] - BLOCKING HOST adobe-premiere.en.softonic.com
2022-11-04 15:55:48,528 INFO CarbonBlocker.Communications.HostsFileManager.?[?] - ************
2022-11-04 15:55:48,530 INFO CarbonBlocker.Communications.HostsFileManager.?[?] - ************
2022-11-04 15:55:48,531 INFO CarbonBlocker.Communications.HostsFileManager.?[?] - BLOCKING HOST download.cnet.com
2022-11-04 15:55:48,532 INFO CarbonBlocker.Communications.HostsFileManager.?[?] - ************
```

## Custom configuration file

Currently the configuration resides in the `appsettings.json` file.

If you compile from source you can restructure the file and add the URLs that
you would like to have blocked when your grid is not clean.

Here is an example of a the file that we use currently ourselves:

- No need for Teamviewer updates when grid is no clean
- I can skip on Youtube when I do not run on clean energy
- No Facebook for me when the grid is not clean. I'll save that for later
- No software downlodas from ZDNet 
- Not Tiktok when the grid is bad
- 
```
{
  "urls": [
    "https://download.teamviewer.com/",
    "https://download.cnet.com",
    "https://www.youtube.com",
    "https://www.tiktok.com",
    "https://www.facebook.com"
  ],
  "limit": 50,
  "delay": 60,
  "hosts_file_path": "C:\\Windows\\System32\\drivers\\etc\\hosts",
  "azure-region_file_path": ".\\azure-regions.json"
}