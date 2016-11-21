using System;
using DotNetRemoteWebDriver.CommandHelpers;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class NewSessionExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            // So this method should instantiate a driver of the given browser type and return
            // the session parentProcessId plus capabilities
            var capabilities = CurrentDesiredCapabilties();
            var driver = GetBrowserName(capabilities);

            switch (driver)
            {
                case "internet explorer":
                    var ieCaps = CapabilityParser.ForInternetExplorer(capabilities);
                    Automator.Driver = new InternetExplorerDriver(ieCaps);
                    break;
                case "chrome":
                    var chromeCaps = CapabilityParser.ForChrome(capabilities);
                    var service = ChromeDriverService.CreateDefaultService();
                    service.EnableVerboseLogging = true;
                    service.SuppressInitialDiagnosticInformation = true;
                    Automator.Driver = new ChromeDriver(service, chromeCaps);
                    break;
                case "firefox":
                    Automator.Driver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile());
                    break;
                default:
                    throw new NotSupportedException("Driver is invalid or not supported: " + driver);
            }

            Services.GetService<IDriverProcessMonitor>().MonitorChildren();

            var response = JsonResponse(ResponseStatus.Success, new CapabilityWrapper(Automator.Driver.Capabilities));
            Logger.Log.Info($"Created a '{driver}' with capabilites: \n" + response);
            return response;
        }

        private string GetBrowserName(JToken capabilities)
        {
            const string browserNameKey = "browserName";
            var browser = capabilities[browserNameKey]?.ToString();
            if(string.IsNullOrEmpty(browser))
                throw new ArgumentException($"No '{browserNameKey}' specified.", browserNameKey);

            return browser.ToLower();
        }

        private JToken CurrentDesiredCapabilties()
        {
            const string capabilitiesKey = "desiredCapabilities";
            if (!ExecutedCommand.Parameters.ContainsKey(capabilitiesKey))
                throw new ArgumentException($"No '{capabilitiesKey}' specified.", capabilitiesKey);

            return ExecutedCommand.Parameters[capabilitiesKey];
        }
    }
}