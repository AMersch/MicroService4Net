using System;
using System.Web.Http;
using MicroService4Net;

namespace Ninject.Web.MicroService4Net
{
    public interface IMicroServiceFactory
    {
        IMicroService Create(int port = 8080, string serviceDisplayName = null, string serviceName = null,
                             Action<HttpConfiguration> configure = null, bool useCors = true);

        IMicroService Create(string ipAddress, int port = 8080, string serviceDisplayName = null, string serviceName = null,
                             Action<HttpConfiguration> configure = null, bool useCors = true);
        
    }
}
