#!/usr/bin/env dotnet script

#r "nuget: Azure.Identity, 1.10.4"
#r "nuget: Azure.ResourceManager.Resources, 1.7.1"
#r "nuget: Azure.ResourceManager.StreamAnalytics, 1.2.0"



// # #!/usr/bin/env dotnet-script
using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.StreamAnalytics;

var subscriptionId = "...";

var armClient = new ArmClient(new DefaultAzureCredential(), subscriptionId);

var subscription = await armClient.GetDefaultSubscriptionAsync();
var resourceGroups = subscription.GetResourceGroups();
var resourceGroup = await resourceGroups.GetAsync("...");

var saj = resourceGroup.GetStreamAnalyticsJob("streamanalyticsjob");

Console.WriteLine("Hello world!");
