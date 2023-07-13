# This project is archived. It was originally created prior to .Net Core, the common logging framework in .Net now, etc. At this point just use that and tie in your logging framework of choice.


# WoodCutter

Woodcutter is a library used to simplify logging on your system. It aims to wrap logging frameworks and give a constant API to them.

## Basic Usage

The system relies on an IoC wrapper called [Canister](https://github.com/JaCraig/Canister). While Canister has a built in IoC container, it's purpose is to actually wrap your container of choice in a way that simplifies setup and usage for other libraries that don't want to be tied to a specific IoC container. Woodcutter uses it to detect and pull in logging providers. As such you must set up Canister in order to use Woodcutter:

    Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
                    .RegisterWoodcutter()
                    .Build();
	
This line is required prior to using the Log class for the first time. Once Canister is set up, you can call the Log class provided:

    var MyLog = Log.Get();
	MyLog = Log.Get("MyLog");
	
The Log class has a single function, Get. This function accepts a string identifying the log that you wish to write to. If no string is provided, it will use "Default". From there you can call LogMessage on the object that is returned:

    Log.Get("MyLog").LogMessage("This is my example message {0} {1} {2}", MessageType.Info, "for", 10, DateTime.Now);
	
The first parameter is your message, the second was the message type, and all remaining parameters are arguments to be used in your message.

## Adding A Logger

The system comes with a built in logging system, however it is very basic and you may wish to add other systems that have greater capabilities. In order to do this all that you need to do is create a class that inherits from ILogger and one that inherits ILog.
	
After the classes are created, you must tell Canister where to look for it. So modify the initialization line accordingly:

    Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
                    .RegisterWoodcutter()
					.AddAssembly(typeof(MyLogger).GetTypeInfo().Assembly)
                    .Build();
	
From there the system will find the new provider and use it when called.

## Installation

The library is available via Nuget with the package name "Woodcutter". To install it run the following command in the Package Manager Console:

Install-Package Woodcutter

## Build Process

In order to build the library you will require the following as a minimum:

1. Visual Studio 2015 with Update 3
2. .Net Core 1.0 SDK

Other than that, just clone the project and you should be able to load the solution and build without too much effort.
