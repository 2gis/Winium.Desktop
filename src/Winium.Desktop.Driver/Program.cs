namespace Winium.Desktop.Driver
{
    #region using

    using System;

    #endregion

    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            var listeningPort = 9999;

            var options = new CommandLineOptions();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
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

            try
            {
                var listener = new Listener(listeningPort);
                Listener.UrnPrefix = options.UrlBase;

                Console.WriteLine("Starting Windows Desktop Driver on port {0}\n", listeningPort);

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
