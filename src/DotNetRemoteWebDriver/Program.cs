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

            var version = typeof (Program).Assembly.GetName().Version.ToString(3);
            Logger.Info($"Running remote web driver version {version}");
            Logger.Info($"Running from {Environment.CurrentDirectory}");

            try
            {
                using (var driverHost = new DriverHost(options.Port, options.UrlBase))
                    driverHost.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                return 1;
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
                Logger.Info($"Logging level is {Logger.CurrentLevel}");
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