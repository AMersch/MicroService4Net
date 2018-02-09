using System;
using Ninject.Modules;

using MicroService4Net.Network;
using Ninject.Web.MicroService4Net.Network;
using MicroService4Net;
using Ninject.Syntax;
using Ninject.Parameters;
using Ninject.Activation;
using System.Web.Http;

namespace Ninject.Web.MicroService4Net
{
    public class MicroServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISelfHostServerFactory>().ToMethod(context => new SelfHostServerNinjectFactory(context));
            Bind<ISelfHostServer>().To<SelfHostServerNinject>();
            Bind<IMicroServiceFactory>().ToMethod(context => new MicroServiceFactory(context));
            Bind<IMicroService>().To<MicroServiceNinject>();
        }

        class SelfHostServerNinjectFactory : ISelfHostServerFactory
        {
            private IKernel m_kernel;

            public SelfHostServerNinjectFactory(IContext context)
            {
                this.m_kernel = context.Kernel;
            }

            public ISelfHostServer Create(string ipaddress, int port, bool callControllersStaticConstractorsOnInit)
            {
                var selfHostServer = this.m_kernel.Get<ISelfHostServer>(new ConstructorArgument("ipaddress", ipaddress),
                                                                        new ConstructorArgument("port", port),
                                                                        new ConstructorArgument("callControllersStaticConstractorsOnInit", callControllersStaticConstractorsOnInit));
                return selfHostServer;
            }
        }

        class MicroServiceFactory : IMicroServiceFactory
        {
            private IKernel m_kernel;

            public MicroServiceFactory(IContext context)
            {
                this.m_kernel = context.Kernel;
            }

            public IMicroService Create(int port = 8080, string serviceDisplayName = null, string serviceName = null, Action<HttpConfiguration> configure = null, bool useCors = true)
            {
                var microService = this.m_kernel.Get<IMicroService>(new ConstructorArgument("port", port),
                                                                    new ConstructorArgument("serviceDisplayName", serviceDisplayName),
                                                                    new ConstructorArgument("serviceName", serviceName),
                                                                    new ConstructorArgument("configure", configure),
                                                                    new ConstructorArgument("useCors", useCors));
                return microService;
            }

            public IMicroService Create(string ipAddress, int port = 8080, string serviceDisplayName = null, string serviceName = null, Action<HttpConfiguration> configure = null, bool useCors = true)
            {
                var microService = this.m_kernel.Get<IMicroService>(new ConstructorArgument("ipAddress", ipAddress),
                                                                    new ConstructorArgument("port", port),
                                                                   new ConstructorArgument("serviceDisplayName", serviceDisplayName),
                                                                   new ConstructorArgument("serviceName", serviceName),
                                                                   new ConstructorArgument("configure", configure),
                                                                   new ConstructorArgument("useCors", useCors));
                return microService;
            }
        }
    }
}
