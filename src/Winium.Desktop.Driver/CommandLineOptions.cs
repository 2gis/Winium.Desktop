namespace Winium.Desktop.Driver
{
    #region using

    using CommandLine;
    using CommandLine.Text;

    #endregion

    internal class CommandLineOptions
    {
        #region Public Properties

        [Option("log-path", Required = false, 
            HelpText = "write server log to file instead of stdout, increases log level to INFO")]
        public string LogPath { get; set; }

        [Option("port", Required = false, DefaultValue = 9999, HelpText = "port to listen on")]
        public int Port { get; set; }

        [Option("url-base", Required = false, HelpText = "base URL path prefix for commands, e.g. wd/url")]
        public string UrlBase { get; set; }

        [Option("verbose", Required = false, HelpText = "log verbosely")]
        public bool Verbose { get; set; }

        [Option("version", Required = false, HelpText = "print version number and exit")]
        public bool Version { get; set; }

        [Option("nodeconfig", Required = false, HelpText = "configuration JSON file to register driver with selenium grid")]
        public string NodeConfig { get; set; }

        [Option("silent", Required = false, HelpText = "log nothing")]
        public bool Silent { get; set; }

        #endregion

        #region Public Methods and Operators

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        #endregion
    }
}
