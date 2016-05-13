﻿using System;
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