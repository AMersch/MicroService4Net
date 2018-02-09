using System;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Linq;

using Microsoft.Owin.Hosting;
using Owin;

using Ninject.Extensions.Logging;

using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using MicroService4Net.Network;
using Ninject.Syntax;

namespace Ninject.Web.MicroService4Net.Network
{
    public class SelfHostServerNinject : ISelfHostServer
    {
        #region Fields

        private readonly StartOptions m_options;
        private IDisposable m_serverDisposable;

        private ILogger m_logger;
        private IResolutionRoot m_resolutionRoot;

        #endregion

        #region C'tor

        public SelfHostServerNinject(IResolutionRoot resolutionRoot, ILogger logger, string ipaddress, int port, bool callControllersStaticConstractorsOnInit)
        {
            this.m_logger = logger;
            this.m_resolutionRoot = resolutionRoot;
            this.m_options = new StartOptions($"http://{ipaddress}:{port}");

            if (callControllersStaticConstractorsOnInit)
                CallControllersStaticConstractors();
        }

        #endregion

        #region Public

        public void Connect(Action<HttpConfiguration> configure, bool useCors)
        {
            this.m_logger?.Info("Connect");

            try
            {
                m_serverDisposable = WebApp.Start(this.m_options, appBuilder => BuildApp(appBuilder, configure, useCors));
            }
            catch (Exception ex)
            {
                this.m_logger?.ErrorException(ex.Message, ex);
            }
        }

        private void BuildApp(IAppBuilder appBuilder, Action<HttpConfiguration> configure, bool useCors)
        {
            var config = new HttpConfiguration();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();

            if (configure != null)
                configure(config);

            if (useCors)
                appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            appBuilder.UseNinjectMiddleware(() => (IKernel)this.m_resolutionRoot).UseNinjectWebApi(config);
        }

        public async void Dispose()
        {
            m_serverDisposable.Dispose();
        }

        #endregion

        #region Private

        private static void CallControllersStaticConstractors()
        {
            foreach (
                var type in
                    Assembly.GetEntryAssembly().DefinedTypes.Where(type => type.IsSubclassOf(typeof(ApiController))))
                InvokeStaticConstractor(type);
        }

        private static void InvokeStaticConstractor(Type type)
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        #endregion
    }
}
