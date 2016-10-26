using log4net;
using log4net.Config;
using System.IO;

namespace DotNetRemoteWebDriver
{
    internal static class Logger
    {
        private static readonly ILog LogInstance = LogManager.GetLogger(".NetRemoteWebDriver");

        public static ILog Log => LogInstance;

        internal static void LoadConfig(string logConfig)
        {
            var logFile = new FileInfo(logConfig);
            XmlConfigurator.Configure(logFile);
        }

        internal static void Silence()
        {
            LogManager.ResetConfiguration();
        }
    }
}