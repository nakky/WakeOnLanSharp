# WakeOnLanSharp

Simple WakeOnLan Utility for C#. 

## Installation

We recommend to insntall Stable Snowball using Nuget.
~~~
Install-Package WakeOnLanSharp
~~~

## Sample Usage

~~~csharp
using WakeOnLanSharp;
...
WakeOnLan wol = new WakeOnLan(9);
...
string macAddress = "00-00-00-00-00-00";
//string macAddress = "00:00:00:00:00:00"; //Separator can be "-" or ":".

byte[] mac = WakeOnLan.ParseMacAddress(macAddress);
wol.SendMagicPacket(mac, IPAddress.Broadcast.ToString());
~~~
