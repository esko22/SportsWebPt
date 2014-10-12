using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Nancy.TinyIoc;
using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Common.Nancy
{
    internal class NancyTinyIocApiResolver : IDependencyResolver
    {
        private readonly TinyIoCContainer _container;
        private bool _disposed;

        public NancyTinyIocApiResolver(TinyIoCContainer container)
        {
            Check.Argument.IsNotNull(container, "TinyIoC Contrainer is null");

            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (TinyIoCResolutionException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType, true);
            }
            catch (TinyIoCResolutionException)
            {
                return Enumerable.Empty<object>();
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }

    }  

}
