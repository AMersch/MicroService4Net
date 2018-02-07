using MicroService4Net.Ninject;

namespace MicroService4Net.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var microService = new MicroServiceNinject();
            microService.Run(args);
        }
    }
}
