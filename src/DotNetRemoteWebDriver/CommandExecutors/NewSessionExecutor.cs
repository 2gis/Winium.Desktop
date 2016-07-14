using System;
using System.Collections.Generic;
using DotNetRemoteWebDriver.CommandHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
            Logger.Info($"Created a '{driver}' with capabilites: \n" + response);
            return response;
        }
    }
}