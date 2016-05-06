using System;
using Newtonsoft.Json;
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
            // the session id plus capabilities
            var capabilities = ExecutedCommand.Parameters["desiredCapabilities"];
            var driver = capabilities["browserName"].ToString().ToLower();

            var serializedCaps = JsonConvert.SerializeObject(capabilities);
            object actualCapabilities = null;
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
                    var firefoxBinary = new FirefoxBinary(@"c:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    Automator.Driver = new FirefoxDriver(firefoxBinary, new FirefoxProfile());
                    break;
                default:
                    throw new NotSupportedException("Driver is invalid or not supported: " + driver);
            }

            return JsonResponse(ResponseStatus.Success, actualCapabilities);
        }
    }
}