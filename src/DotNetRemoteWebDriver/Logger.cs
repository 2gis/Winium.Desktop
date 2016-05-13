using System.ComponentModel;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace DotNetRemoteWebDriver
{
    internal static class Logger
    {
        private const string LayoutFormat = "${date:format=HH\\:mm\\:ss} [${level:uppercase=true}] ${message}";

        private static readonly NLog.Logger Log = LogManager.GetLogger("outerdriver");

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
            var target = new ConsoleTarget {Layout = LayoutFormat};

            SimpleConfigurator.ConfigureForTargetLogging(target, verbose ? LogLevel.Debug : LogLevel.Info);
            LogManager.ReconfigExistingLoggers();
        }

        public static void TargetFile(string fileName, bool verbose)
        {
            var target = new FileTarget {Layout = LayoutFormat, FileName = fileName};

            SimpleConfigurator.ConfigureForTargetLogging(target, verbose ? LogLevel.Debug : LogLevel.Info);
            LogManager.ReconfigExistingLoggers();
        }

        public static void TargetNull()
        {
            SimpleConfigurator.ConfigureForTargetLogging(new NullTarget());
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
    }
}