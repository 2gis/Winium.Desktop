using System;
using CommandLine;

namespace DotNetRemoteWebDriver
{
    internal class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            CommandLineOptions options;
            if (!TryInitializeOptions(args, out options))
                return 1;

            IDriverProcessMonitor processMonitor = null;
            try
            {
                new PriorCleanup { Port = options.Port}.Run();

                var services = new ServiceProvider();
                processMonitor = new DriverProcessMonitor();
                services.Register<IDriverProcessMonitor>(processMonitor);
                var listener = new Listener(options.Port, services);

                if (!string.IsNullOrEmpty(options.UrlBase))
                    options.UrlBase = "/" + options.UrlBase.Trim('/');
                Listener.UrnPrefix = options.UrlBase;

                Console.WriteLine("Starting remote web driver on port {0}\n", options.Port);

                listener.StartListening();
                return 0;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                return 1;
            }
            finally
            {
                processMonitor?.Dispose();
            }
        }

        private static bool TryInitializeOptions(string[] args, out CommandLineOptions options)
        {
            try
            {
                options = new CommandLineOptions();
                if (!Parser.Default.ParseArguments(args, options))
                    return false;

                if (!string.IsNullOrEmpty(options.LogPath))
                    Logger.TargetFile(options.LogPath, options.Verbose);
                else if (options.Silent)
                    Logger.TargetNull();
                else
                    Logger.TargetConsole(options.Verbose);

                return true;
            }
            catch (Exception e)
            {
                options = null;
                Console.Error.WriteLine("Failed to initialize application: " + e.Message);
                Console.Error.WriteLine("Failed to initialize application: " + e.StackTrace);
                return false;
            }
        }
    }
}