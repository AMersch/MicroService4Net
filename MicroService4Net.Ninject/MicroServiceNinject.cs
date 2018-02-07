using System;
using System.Reflection;
using System.Web.Http;

using MicroService4Net.Network;
using MicroService4Net.Ninject.Network;

using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Parameters;

namespace MicroService4Net.Ninject
{
    public class MicroServiceNinject : MicroService
    {
        private ILogger m_logger;

        public MicroServiceNinject(int port = 8080, string serviceDisplayName = null, string serviceName = null,
            Action<HttpConfiguration> configure = null, bool useCors = true)
            : base(port, serviceDisplayName, serviceName, configure, useCors)
        {
            Initialize();
        }

        /// <param name="ipAddress">Valid IP address (ie. localhost, *, 192.168.0.1, etc.). * binds to all IPs and it may be security issue (VPNs etc)</param>
        public MicroServiceNinject(string ipAddress, int port = 8080, string serviceDisplayName = null, string serviceName = null,
            Action<HttpConfiguration> configure = null, bool useCors = true)
            :base(ipAddress, port, serviceDisplayName, serviceName, configure, useCors)
        {
            Initialize();
        }

        private void Initialize()
        {
            CompositionRoot.Wire(new MicroServiceDIModule());

            var loggerFactory = CompositionRoot.Resolve<ILoggerFactory>();
            this.m_logger = loggerFactory.GetCurrentClassLogger();
        }

        protected override void Start(Action<HttpConfiguration> configure, bool useCors)
        {
            _selfHostServer = CompositionRoot.Resolve<ISelfHostServer>(new ConstructorArgument("ipaddress", this.IpAddress),
                                                                       new ConstructorArgument("port", this.Port),
                                                                       new ConstructorArgument("callControllersStaticConstractorsOnInit", true));

            _selfHostServer.Connect(configure, useCors);
            this.m_logger?.Debug($"Service {this.ServiceDisplayName} started on {this.IpAddress}:{this.Port}");

            base.OnServiceStarted();
        }
    }
}
