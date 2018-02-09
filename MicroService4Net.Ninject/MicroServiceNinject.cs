using System;
using System.Web.Http;
using MicroService4Net;
using MicroService4Net.Network;

using Ninject.Extensions.Logging;
using Ninject.Parameters;

namespace Ninject.Web.MicroService4Net
{
    public class MicroServiceNinject : MicroService
    {
        private ILogger m_logger;
        private ISelfHostServerFactory m_selfHostServerFactory;

        public MicroServiceNinject(ILogger logger, ISelfHostServerFactory selfHostServerFactory, int port = 8080, string serviceDisplayName = null, string serviceName = null,
            Action<HttpConfiguration> configure = null, bool useCors = true)
            : base(port, serviceDisplayName, serviceName, configure, useCors)
        {
            this.m_logger = logger;
            this.m_selfHostServerFactory = selfHostServerFactory;
        }

        /// <param name="ipAddress">Valid IP address (ie. localhost, *, 192.168.0.1, etc.). * binds to all IPs and it may be security issue (VPNs etc)</param>
        public MicroServiceNinject(ILogger logger, ISelfHostServerFactory selfHostServerFactory, string ipAddress, int port = 8080, string serviceDisplayName = null, string serviceName = null,
            Action<HttpConfiguration> configure = null, bool useCors = true)
            :base(ipAddress, port, serviceDisplayName, serviceName, configure, useCors)
        {
            this.m_logger = logger;
            this.m_selfHostServerFactory = selfHostServerFactory;
        }

        protected override void Start(Action<HttpConfiguration> configure, bool useCors)
        {
            _selfHostServer = this.m_selfHostServerFactory.Create(this.IpAddress, this.Port, true);

            _selfHostServer.Connect(configure, useCors);
            this.m_logger?.Debug($"Service {this.ServiceDisplayName} started on {this.IpAddress}:{this.Port}");

            base.OnServiceStarted();
        }
    }
}
