using System;
using MicroService4Net.Network;

namespace Ninject.Web.MicroService4Net
{
    public interface ISelfHostServerFactory
    {
        ISelfHostServer Create(string ipaddress, int port, bool callControllersStaticConstractorsOnInit);
    }
}
