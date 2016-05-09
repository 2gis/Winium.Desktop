#region using

using System;
using System.Security.RightsManagement;
using CommandLine;

#endregion

namespace DotNetRemoteWebDriver
{
    #region using

    

    #endregion

    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            var listeningPort = 4444;

            var options = new CommandLineOptions();
            if (Parser.Default.ParseArguments(args, options))
            {
                if (options.Port.HasValue)
                {
                    listeningPort = options.Port.Value;
                }
            }

            if (options.LogPath != null)
            {
                Logger.TargetFile(options.LogPath, options.Verbose);
            }
            else if (!options.Silent)
            {
                Logger.TargetConsole(options.Verbose);
            }
            else
            {
                Logger.TargetNull();
            }

            IDriverProcessMonitor processMonitor = null;
            try
            {
                var services = new ServiceProvider();
                processMonitor = new DriverProcessMonitor();
                services.Register<IDriverProcessMonitor>(processMonitor);
                var listener = new Listener(listeningPort, services);
                Listener.UrnPrefix = options.UrlBase;

                Console.WriteLine("Starting Windows Desktop Driver on port {0}\n", listeningPort);

                listener.StartListening();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                throw;
            }
            finally
            {
                processMonitor?.Dispose();
            }
        }

        #endregion
    }
}