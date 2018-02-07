using System.Web.Http;
using Ninject;
using Ninject.Extensions.Logging;

namespace MicroService4Net.Example.Controllers
{
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
}
