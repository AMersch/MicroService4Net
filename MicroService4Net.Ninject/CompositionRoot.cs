using System;
using System.Collections.Generic;

using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace MicroService4Net.Ninject
{
    public class CompositionRoot
    {

        /// <summary>The ninject kernel.</summary>
        private static IKernel _ninjectKernel;

        public static IKernel Kernel => _ninjectKernel;


        /// <summary>Wires the given module. Creates a new kernel, if no kernel exists.</summary>
        ///
        /// <remarks>Mersch, 06.04.2017.</remarks>
        ///
        /// <param name="module">The module.</param>

        public static void Wire(INinjectModule module)
        {
            if (_ninjectKernel == null)
            {
                _ninjectKernel = new StandardKernel(module);
            }
            else
            {
                _ninjectKernel.Load(new List<INinjectModule> { module });
            }
        }

        /// <summary> Resolves a specific type.</summary>
        ///
        /// <remarks> Mersch, 06.04.2017.</remarks>
        ///
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="p">A variable-length parameters list containing p.</param>
        ///
        /// <returns>A T.</returns>

        public static T Resolve<T>(string name, params Parameter[] p)
        {
            return _ninjectKernel.Get<T>(name, p);
        }

        /// <summary>Resolves a specific type.</summary>
        ///
        /// <remarks>Mersch, 06.04.2017.</remarks>
        ///
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="p">A variable-length parameters list containing p.</param>
        ///
        /// <returns>A T. </returns>

        public static T Resolve<T>(params Parameter[] p)
        {
            return _ninjectKernel.Get<T>(p);
        }

        /// <summary>
        /// Resolves a specific type.
        /// </summary>
        /// <remarks>Mersch, 06.04.2017.</remarks>
        /// <param name="t">The type.</param>
        /// <param name="p">The parameters.</param>
        /// <returns></returns>
        public static object Resolve(Type t, params Parameter[] p)
        {
            return _ninjectKernel.Get(t, p);
        }

        /// <summary>Gets the bindings.</summary>
        ///
        /// <remarks>Mersch, 06.04.2017.</remarks>
        ///
        /// <param name="t">A Type to process.</param>
        ///
        /// <returns>The bindings.</returns>

        public static object GetBindings(Type t)
        {
            return _ninjectKernel.GetBindings(t);
        }
    }
}
