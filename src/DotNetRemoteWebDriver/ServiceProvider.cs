using System;
using System.Collections.Generic;

namespace DotNetRemoteWebDriver
{
    /// <summary>Very, very simple service provider implementation</summary>
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register<TType>(object instance)
        {
            _services.Add(typeof (TType), instance);
        }

        public object GetService(Type serviceType)
        {
            object instance;
            if (!_services.TryGetValue(serviceType, out instance))
                throw new Exception("No is registered for type: " + serviceType.Name);

            return instance;
        }
    }
}