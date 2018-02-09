using System;

namespace MicroService4Net
{
    public interface IMicroService
    {
        event Action ServiceStarted;
        event Action ServiceStopped;

        void Run(string[] args);
    }
}