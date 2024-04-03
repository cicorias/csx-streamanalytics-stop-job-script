#!/usr/bin/env dotnet script

#r "nuget: Azure.Identity, 1.10.4"
#r "nuget: Azure.ResourceManager.Resources, 1.7.1"
#r "nuget: Azure.ResourceManager.StreamAnalytics, 1.2.0"

using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.StreamAnalytics;

var cred = new DefaultAzureCredential();
// authenticate your client
ArmClient client = new ArmClient(cred);
// https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/streamanalytics/Azure.ResourceManager.StreamAnalytics/samples/Generated/Samples/Sample_StreamingJobResource.cs
// 
// this example assumes you already have this StreamingJobResource created on azure
// for more information of creating StreamingJobResource, please refer to the document of StreamingJobResource
string subscriptionId = GetRequiredEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
string resourceGroupName = GetRequiredEnvironmentVariable("AZURE_RESOURCE_GROUP");
string jobName = GetRequiredEnvironmentVariable("AZURE_STREAMING_JOB_NAME");
ResourceIdentifier streamingJobResourceId = StreamingJobResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, jobName);
StreamingJobResource streamingJob = client.GetStreamingJobResource(streamingJobResourceId);

// invoke the operation
await streamingJob.StopAsync(WaitUntil.Completed);

Console.WriteLine($"Succeeded");

string GetRequiredEnvironmentVariable(string variableName)
{
  string value = Environment.GetEnvironmentVariable(variableName);
  if (string.IsNullOrEmpty(value))
  {
    Console.WriteLine($"Error: Environment variable '{variableName}' not found.");
    Environment.Exit(1); // Exit the application with an error code
  }
  return value;
}
