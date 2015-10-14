namespace Winium.Desktop.Driver
{
    #region using

    using System.ComponentModel;

    using NLog;
    using NLog.Targets;

    #endregion

    internal static class Logger
    {
        #region Constants

        private const string LayoutFormat = "${date:format=HH\\:mm\\:ss} [${level:uppercase=true}] ${message}";

        #endregion

        #region Static Fields

        private static readonly NLog.Logger Log = LogManager.GetLogger("outerdriver");

        #endregion

        #region Public Methods and Operators

        public static void Debug([Localizable(false)] string message, params object[] args)
        {
            Log.Debug(message, args);
        }

        public static void Error([Localizable(false)] string message, params object[] args)
        {
            Log.Error(message, args);
        }

        public static void Fatal([Localizable(false)] string message, params object[] args)
        {
            Log.Fatal(message, args);
        }

        public static void Info([Localizable(false)] string message, params object[] args)
        {
            Log.Info(message, args);
        }

        public static void TargetConsole(bool verbose)
        {
            var target = new ConsoleTarget { Layout = LayoutFormat };

            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, verbose ? LogLevel.Debug : LogLevel.Fatal);
            LogManager.ReconfigExistingLoggers();
        }

        public static void TargetFile(string fileName, bool verbose)
        {
            var target = new FileTarget { Layout = LayoutFormat, FileName = fileName };

            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, verbose ? LogLevel.Debug : LogLevel.Info);
            LogManager.ReconfigExistingLoggers();
        }

        public static void TargetNull()
        {
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(new NullTarget());
            LogManager.ReconfigExistingLoggers();
        }

        public static void Trace([Localizable(false)] string message, params object[] args)
        {
            Log.Trace(message, args);
        }

        public static void Warn([Localizable(false)] string message, params object[] args)
        {
            Log.Warn(message, args);
        }

        #endregion
    }
}
