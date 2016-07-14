using System;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace DotNetRemoteWebDriver.CommandHelpers
{
    public class CapabilityParser
    {
        public FirefoxOptions ForFirefox(JToken capabilities)
        {
            var options = new FirefoxOptions();
            return options;
        }

        public static InternetExplorerOptions ForInternetExplorer(JToken capabilities)
        {
            return new InternetExplorerOptions
            {
                IgnoreZoomLevel = Get<bool>(capabilities["ignoreZoomSetting"]),
                BrowserAttachTimeout = TimeSpan.FromMilliseconds(Get<int>(capabilities["browserAttachTimeout"])),
                BrowserCommandLineArguments = capabilities["ie.browserCommandLineSwitches"]?.ToString(),
                ElementScrollBehavior =
                    Get<InternetExplorerElementScrollBehavior>(capabilities["elementScrollBehavior"]),
                EnableFullPageScreenshot = Get<bool>(capabilities["enableFullPageScreenshot"]),
                EnableNativeEvents = Get<bool>(capabilities["enableNativeEvents"]),
                EnablePersistentHover = Get<bool>(capabilities["enablePersistentHover"]),
                EnsureCleanSession = Get<bool>(capabilities["ie.ensureCleanSession"]),
                FileUploadDialogTimeout = TimeSpan.FromMilliseconds(Get<int>(capabilities["fileUploadDialogTimeut"])),
                ForceCreateProcessApi = Get<bool>(capabilities["ie.forceCreateProcessApi"]),
                ForceShellWindowsApi = Get<bool>(capabilities["forceShellWindowsApi"]),
                InitialBrowserUrl = capabilities["initialBrowserUrl"]?.ToString(),
                IntroduceInstabilityByIgnoringProtectedModeSettings =
                    Get<bool>(capabilities["ignoreProtectedModeSettings"]),
                PageLoadStrategy = Get<InternetExplorerPageLoadStrategy>(capabilities["pageLoadStrategy"]),
                RequireWindowFocus = Get<bool>(capabilities["requireWindowFocus"]),
                UnexpectedAlertBehavior =
                    Get<InternetExplorerUnexpectedAlertBehavior>(capabilities["unexpectedAlertBehavior"])
            };
        }

        public static ChromeOptions ForChrome(JToken capabilities)
        {
            var options = new ChromeOptions
            {
                BinaryLocation = capabilities["binary"]?.ToString(),
                DebuggerAddress = capabilities["debuggerAddress"]?.ToString(),
                LeaveBrowserRunning = Get<bool>(capabilities["detach"]),
                MinidumpPath = capabilities["minidumpPath"]?.ToString(),
            };

            var arguments = capabilities["args"]?.Select(t => t.ToString()).ToList();
            if(arguments != null && arguments.Any())
                options.AddArguments(arguments);

            var extensions = capabilities["extensions"]?.Select(t => t.ToString()).ToList();
            if(extensions != null && extensions.Any())
                options.AddExtensions(extensions);

            return options;
        }

        private static TOutput Get<TOutput>(JToken jToken)
        {
            if (jToken == null)
                return default(TOutput);

            return (TOutput)Convert.ChangeType(jToken.ToString(), typeof(TOutput));
        }
    }
}
