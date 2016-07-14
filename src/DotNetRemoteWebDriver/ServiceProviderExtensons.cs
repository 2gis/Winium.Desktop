using System;

namespace DotNetRemoteWebDriver
{
    public static class ServiceProviderExtensons
    {
        public static TServiceType GetService<TServiceType>(this IServiceProvider serviceProvider)
            where TServiceType : class
        {
            return (TServiceType) serviceProvider.GetService(typeof (TServiceType));
        }
    }
}