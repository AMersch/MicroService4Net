using System;
using System.Web.Http;

namespace MicroService4Net.Network
{
    public interface ISelfHostServer
    {
        void Connect(Action<HttpConfiguration> configure, bool useCors);
        void Dispose();
    }
}