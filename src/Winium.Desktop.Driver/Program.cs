namespace Winium.Desktop.Driver
{
    using CommandLine;
    using CommandLine.Text;
    #region using

    using System;
    using System.Collections.Generic;

    #endregion

    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            // args usage
            args = new[] { /*"--log-path=null",*/ "--port=80", "--url-base=null", "--verbose", /*"--silent=false"*/ };

            var listeningPort = 9999;
            var parser = new Parser();
            var result = parser.ParseArguments<CommandLineOptions>(args);

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(options =>
                {
                    if (options.Port.HasValue)
                    {
                        listeningPort = options.Port.Value;
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

                        Console.WriteLine($"Starting Windows Desktop Driver on port {listeningPort}...");
                        listener.StartListening();
                    }
                    catch (Exception ex)
                    {
                        Logger.Fatal($"Failed to start driver: {ex}");
                        throw;
                    }
                })
                .WithNotParsed(errs =>
                {
                    HelpText.AutoBuild(result, h =>
                    {
                        h.AddOptions(result);
                        Logger.Error(h);
                        return h;
                    }, e =>
                    {
                        Logger.Error(e.HelpText);
                        return e;
                    });
                });
        }

        #endregion
    }
}
