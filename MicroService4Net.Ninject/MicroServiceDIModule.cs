using System;
using Ninject.Modules;

using MicroService4Net.Network;
using MicroService4Net.Ninject.Network;

namespace MicroService4Net.Ninject
{
    internal class MicroServiceDIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISelfHostServer>().To<SelfHostServerNinject>();
        }
    }
}
