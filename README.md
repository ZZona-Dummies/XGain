# XGain  [![Build status](https://ci.appveyor.com/api/projects/status/j3gx8dlsky0nb7b0?svg=true)](https://ci.appveyor.com/project/LukaszPyrzyk/xgain) [![codecov.io](https://codecov.io/github/lukasz-pyrzyk/XGain/coverage.svg?branch=master)](https://codecov.io/github/lukasz-pyrzyk/Xgain?branch=master)

### Description
Xgain is a TCP/IP wrapper based on TcpListener and Socket class. Usage of IServer and ISocket interfaces allows you to abstract network layer. 

### Usage
```csharp
using(IServer server = new XGainServer(IPAddress.Any, 5000, () => new SocketProcessor())
{
    server.OnNewMessage += (sender, message) => {} // assign your method
    Task worker = server.Start();
    worker.Wait();
}
```
License
----
MIT
