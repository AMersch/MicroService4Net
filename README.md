MicroService4Net Ninject Extension
==================================
A fork of TheCodeCleaner's MicroService4Net with Ninject support. 

Additional Requirements
-----------------------
* Ninject
* Ninject.Extensions.Logging
* Ninject.Extensions.NamedScope
* Ninject.Web.Common
* Ninject.Web.Common.OwinHost
* Ninject.Web.WebApi
* Ninject.Web.WebApi.OwinHost

Repository
----------
This project can be found at https://github.com/AMersch/MicroService4Net

Examples
--------

* Program.cs
```C#
class Program
{
    static void Main(string[] args)
    {
        using (IKernel kernel = new StandardKernel())
        {
            var microServiceFactory = kernel.Get<IMicroServiceFactory>();
            var microService = microServiceFactory.Create();

            microService.Run(args);
        }
    }
}
```

* ExampleController.cs
```C#
[RoutePrefix("api/v1/Example")]
public class ExampleController : ApiController
{
    ILogger m_logger;

    public ExampleController(ILogger logger)
        : base()
    {
        this.m_logger = logger;
        this.m_logger?.Debug("Ctor");
    }

    [Route("")]
    public string GetExample()
    {
        this.m_logger?.Debug("GetExample");
        return "Example";
    }
}
```

Credits
-------
Based on [MicroService4Net](https://github.com/TheCodeCleaner/MicroService4Net) by TheCodeCleaner.
