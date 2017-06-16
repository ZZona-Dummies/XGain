# XGain  [![Build status](https://ci.appveyor.com/api/projects/status/j3gx8dlsky0nb7b0?svg=true)](https://ci.appveyor.com/project/LukaszPyrzyk/xgain) [![codecov.io](https://codecov.io/github/lukasz-pyrzyk/XGain/coverage.svg?branch=master)](https://codecov.io/github/lukasz-pyrzyk/Xgain?branch=master)

### Description
XGain is simple and small TCP/IP server. 

Things todo:
- [ ] Optimize network layer to reuse bytes
- [ ] Add retry logic using Polly

### Starting the client
```csharp
var message = new byte[1024];
var client = new XGainClient(address, port);
await client.SendAsync(message);
```

### Starting the server
```csharp
var server = new XGainServer(address, port);

server.OnNewMessage += (sender, args) =>
{
	YourInternalProcessingMethod(args);
};

server.OnStart += (sender, args) =>
{
	LogInformationAboutStartup(args);
};

server.OnError += (sender, args) =>
{
	LogInformationAboutError(args);
}

try
{
	server.Start();
	// sleep, return or delay dispose in finally block
}
finally
{
	server.Dispose();	
}
```

License
----
MIT
