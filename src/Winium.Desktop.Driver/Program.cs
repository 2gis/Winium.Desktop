namespace Winium.Desktop.Driver
{
    #region using

    using System;
    using Winium.Desktop.Driver.CommandHelpers;

    #endregion

    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            CommandLine.Parser.Default.ParseArgumentsStrict(args, options);

            var appName = typeof(Program).Assembly.GetName().Name;
            var versionInfo = string.Format("{0}, {1}", appName, new BuildInfo());

            if (options.Version)
            {
                Console.WriteLine(versionInfo);
                Environment.Exit(0);
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

            Logger.Info(versionInfo);

            try
            {
                var listener = new Listener(options.Port, options.UrlBase, options.NodeConfig);

                Console.WriteLine("Starting {0} on port {1}\n", appName, listener.Port);

                listener.StartListening();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
