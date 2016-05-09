using System;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

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

            var serializedCaps = JsonConvert.SerializeObject(capabilities);
            object actualCapabilities;
            switch (driver)
            {
                case "internet explorer":
                    var ieOptions = JsonConvert.DeserializeObject<InternetExplorerOptions>(serializedCaps);
                    actualCapabilities = ieOptions;
                    Automator.Driver = new InternetExplorerDriver(ieOptions);
                    break;
                case "chrome":
                    var chromeOptions = JsonConvert.DeserializeObject<ChromeOptions>(serializedCaps);
                    actualCapabilities = chromeOptions;
                    Automator.Driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefox":
                    var ffOptions = JsonConvert.DeserializeObject<FirefoxOptions>(serializedCaps);
                    actualCapabilities = ffOptions;
                    Automator.Driver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile());
                    break;
                default:
                    throw new NotSupportedException("Driver is invalid or not supported: " + driver);
            }

            Services.GetService<IDriverProcessMonitor>().MonitorChildren();

            return JsonResponse(ResponseStatus.Success, actualCapabilities);
        }
    }
}