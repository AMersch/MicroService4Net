using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Web.MicroService4Net;

namespace MicroService4Net.Example
{
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
}
