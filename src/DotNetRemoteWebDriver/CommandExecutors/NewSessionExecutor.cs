using System;
using DotNetRemoteWebDriver.CommandHelpers;
using Newtonsoft.Json;
using NLog.LayoutRenderers;
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
            var capabilities = ExecutedCommand.Parameters["desiredCapabilities"];
            var driver = capabilities["browserName"].ToString().ToLower();

            object actualCapabilities;
            switch (driver)
            {
                case "internet explorer":
                    actualCapabilities = CapabilityParser.ForInternetExplorer(capabilities);
                    Automator.Driver = new InternetExplorerDriver((InternetExplorerOptions)actualCapabilities);
                    break;
                case "chrome":
                    var options = CapabilityParser.ForChrome(capabilities);
                    actualCapabilities = options;
                    var service = ChromeDriverService.CreateDefaultService();
                    service.EnableVerboseLogging = true;
                    //service.HideCommandPromptWindow = true;
                    service.SuppressInitialDiagnosticInformation = true;
                    Automator.Driver = new ChromeDriver(service, (ChromeOptions)actualCapabilities);
                    break;
                case "firefox":
                    actualCapabilities = new FirefoxOptions();
                    Automator.Driver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile());
                    break;
                default:
                    throw new NotSupportedException("Driver is invalid or not supported: " + driver);
            }

            Services.GetService<IDriverProcessMonitor>().MonitorChildren();

            Logger.Info($"Created a '{driver}' with capabilites: \n" + JsonConvert.SerializeObject(actualCapabilities, Formatting.Indented));
            return JsonResponse(ResponseStatus.Success, actualCapabilities);
        }
    }
}