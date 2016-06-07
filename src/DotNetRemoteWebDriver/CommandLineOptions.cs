using CommandLine;
using CommandLine.Text;

namespace DotNetRemoteWebDriver
{
    internal class CommandLineOptions
    {
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        [Option("log-path", Required = false, HelpText = "write server log to file instead of stdout, increases log level to INFO")]
        public string LogPath { get; set; }

        [Option("port", Required = false, HelpText = "port to listen on", DefaultValue = 4444)]
        public int Port { get; set; }

        [Option("silent", Required = false, HelpText = "log nothing", DefaultValue = false)]
        public bool Silent { get; set; }

        [Option("url-base", Required = false, HelpText = "base URL path prefix for commands, e.g. wd/url")]
        public string UrlBase { get; set; }

        [Option("verbose", Required = false, HelpText = "log verbosely", DefaultValue = false)]
        public bool Verbose { get; set; }
    }
}