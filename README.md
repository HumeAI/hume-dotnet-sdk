# Hume C# Library

[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-Built%20with%20Fern-brightgreen)](https://buildwithfern.com?utm_source=github&utm_medium=github&utm_campaign=readme&utm_source=https%3A%2F%2Fgithub.com%2FHumeAI%2Fhume-dotnet-sdk)
[![nuget shield](https://img.shields.io/nuget/v/Hume)](https://nuget.org/packages/Hume)

The Hume C# library provides convenient access to the Hume APIs from C#.

## Table of Contents

- [Requirements](#requirements)
- [Installation](#installation)
- [Reference](#reference)
- [Usage](#usage)
- [Exception Handling](#exception-handling)
- [Pagination](#pagination)
- [Advanced](#advanced)
  - [Retries](#retries)
  - [Timeouts](#timeouts)
  - [Raw Response](#raw-response)
  - [Forward Compatible Enums](#forward-compatible-enums)
- [Contributing](#contributing)

## Requirements

This SDK requires:

## Installation

```sh
dotnet add package Hume
```

## Reference

A full reference for this library is available [here](https://github.com/HumeAI/hume-dotnet-sdk/blob/HEAD/./reference.md).

## Usage

Instantiate and use the client with the following:

```csharp
using Hume.EmpathicVoice;
using Hume;

var client = new HumeClient("API_KEY");
await client.EmpathicVoice.ControlPlane.SendAsync(
    "chat_id",
    new SessionSettings { Type = "session_settings" }
);
```

## Exception Handling

When the API returns a non-success status code (4xx or 5xx response), a subclass of the following error
will be thrown.

```csharp
using Hume;

try {
    var response = await client.EmpathicVoice.ControlPlane.SendAsync(...);
} catch (HumeClientApiException e) {
    System.Console.WriteLine(e.Body);
    System.Console.WriteLine(e.StatusCode);
}
```

## Pagination

List endpoints are paginated. The SDK provides an async enumerable so that you can simply loop over the items:

```csharp
using Hume.EmpathicVoice;
using Hume;

var client = new HumeClient("API_KEY");
var items = await client.EmpathicVoice.ChatGroups.ListChatGroupsAsync(
    new ChatGroupsListChatGroupsRequest
    {
        PageNumber = 0,
        PageSize = 1,
        AscendingOrder = true,
        ConfigId = "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    }
);

await foreach (var item in items)
{
    // do something with item
}
```

## Advanced

### Retries

The SDK is instrumented with automatic retries with exponential backoff. A request will be retried as long
as the request is deemed retryable and the number of retry attempts has not grown larger than the configured
retry limit (default: 2).

A request is deemed retryable when any of the following HTTP status codes is returned:

- [408](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/408) (Timeout)
- [429](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/429) (Too Many Requests)
- [5XX](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500) (Internal Server Errors)

Use the `MaxRetries` request option to configure this behavior.

```csharp
var response = await client.EmpathicVoice.ControlPlane.SendAsync(
    ...,
    new RequestOptions {
        MaxRetries: 0 // Override MaxRetries at the request level
    }
);
```

### Timeouts

The SDK defaults to a 30 second timeout. Use the `Timeout` option to configure this behavior.

```csharp
var response = await client.EmpathicVoice.ControlPlane.SendAsync(
    ...,
    new RequestOptions {
        Timeout: TimeSpan.FromSeconds(3) // Override timeout to 3s
    }
);
```

### Raw Response

Access raw HTTP response data (status code, headers, URL) alongside parsed response data using the `.WithRawResponse()` method.

```csharp
using Hume;

// Access raw response data (status code, headers, etc.) alongside the parsed response
var result = await client.EmpathicVoice.ControlPlane.SendAsync(...).WithRawResponse();

// Access the parsed data
var data = result.Data;

// Access raw response metadata
var statusCode = result.RawResponse.StatusCode;
var headers = result.RawResponse.Headers;
var url = result.RawResponse.Url;

// Access specific headers (case-insensitive)
if (headers.TryGetValue("X-Request-Id", out var requestId))
{
    System.Console.WriteLine($"Request ID: {requestId}");
}

// For the default behavior, simply await without .WithRawResponse()
var data = await client.EmpathicVoice.ControlPlane.SendAsync(...);
```

### Forward Compatible Enums

This SDK uses forward-compatible enums that can handle unknown values gracefully.

```csharp
using Hume.EmpathicVoice;

// Using a built-in value
var builtInTool = BuiltInTool.WebSearch;

// Using a custom value
var customBuiltInTool = BuiltInTool.FromCustom("custom-value");

// Using in a switch statement
switch (builtInTool.Value)
{
    case BuiltInTool.Values.WebSearch:
        Console.WriteLine("WebSearch");
        break;
    default:
        Console.WriteLine($"Unknown value: {builtInTool.Value}");
        break;
}

// Explicit casting
string builtInToolString = (string)BuiltInTool.WebSearch;
BuiltInTool builtInToolFromString = (BuiltInTool)"web_search";
```

## Contributing

While we value open-source contributions to this SDK, this library is generated programmatically.
Additions made directly to this library would have to be moved over to our generation code,
otherwise they would be overwritten upon the next generated release. Feel free to open a PR as
a proof of concept, but know that we will not be able to merge it as-is. We suggest opening
an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!