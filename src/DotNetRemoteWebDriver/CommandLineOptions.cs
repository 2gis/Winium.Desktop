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

        [Option("log-config", Required = false, HelpText = "path to the log4net config file, overriding the standard log config")]
        public string LogConfig { get; set; }

        [Option("port", Required = false, HelpText = "port to listen on", DefaultValue = 4444)]
        public int Port { get; set; }

        [Option("silent", Required = false, HelpText = "log nothing", DefaultValue = false)]
        public bool Silent { get; set; }

        [Option("url-base", Required = false, HelpText = "base URL path prefix for commands, e.g. wd/url")]
        public string UrlBase { get; set; }
    }
}