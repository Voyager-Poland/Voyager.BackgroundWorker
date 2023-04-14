# Voyager.BackgroundWorker

---
 The service continuously executes tasks based on time intervals. Protects from overlapping between new requests during the previous operation hasn't been finished. Can be configured to be triggered through http request. Can be extended for purposes to carry out customized control about exclusive executive the one task in an environment.

## 🏁 Getting Started 

### Prerequisites
The library coperate with the WebApplicationBuilder or HostApplicationBuilder. By the default, the hosts contain the required Dependency Injection.

## 🔧 How to start

To start using the service is required to implement the interface:


```.NET CLI 
namespace Voyager.BackgroundWorker
{
	public interface WorkerTask
	{
		void Run(CancellationToken cancellationToken);
	}
}
```

Assume that is implemented the task:

```.NET CLI 
public class MyTask : Voyager.BackgroundWorker.WorkerTask
{
	public void Run(CancellationToken cancellationToken)
	{
		// The logic goes here!
	}
}
```
Then service is configure by the method:

```.NET CLI 
var appBuilder = Host.CreateDefaultBuilder(null);
appBuilder.ConfigureServices((services) =>
  {
    services.AddBackgroundWorker().AddWorker<MyTask>();
  });
host = appBuilder.Build();
host.Start();
```
Then the service starts executing the work task with a default interval every 60 seconds.

It is possoble to change the interval by setting:

```.NET CLI 
services.AddBackgroundWorker()
.AddWorker<MyTask>()
.SetTimeUpToWakeUp(300);
```

## 🔧 How to trigger the task

It is an option to configure the service process with the additional requests of processing beyond times interval. 

```.NET CLI 
{
  var appBuilder = Host.CreateDefaultBuilder(null);
  appBuilder.ConfigureServices((services) =>
  {
    services.AddBackgroundWorker().AddWorker<MyTask>();
  });
  appBuilder.ConfigureWebHostDefaults((webConfigure) =>
  {
		webConfigure.UseStartup<Startup>();
  });
  host = appBuilder.Build();
  host.Start();
}

class Startup
{
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapBackgroundWorker();
    });
  }
}
```
## 🔧 How to configure external lock

After custom implementation the class Voyager.BackgroundWorker.ExternalLock. There is the possibility to use the logic in the service by registration through the method:

```.NET CLI
services.AddBackgroundWorker()
.AddWorker<MyTask>()
.AddExternalLock<MyLock>();
```


## ✍️ Authors 

- [@andrzejswistowski](https://github.com/AndrzejSwistowski) - Idea & work. Please let me know if you find out an error or suggestions.

[contributors](https://github.com/Voyager-Poland).

## 🎉 Acknowledgements 

- Przemysław Wróbel - for the icon.