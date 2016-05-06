#region using

using System;
using DotNetRemoteWebDriver.Automator;
using DotNetRemoteWebDriver.Input;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Winium.Cruciatus;
using Winium.Cruciatus.Settings;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

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

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = Automator.ActualCapabilities.App;
            var appArguments = Automator.ActualCapabilities.Arguments;

            Automator.Application = new Application(appPath);
            if (!debugDoNotDeploy)
            {
                Automator.Application.Start(appArguments);
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            Automator.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }

        private Capabilities ParseCapabilities()
        {
            var requestedCaps = ExecutedCommand.Parameters["desiredCapabilities"];
            var caps = JsonConvert.SerializeObject(requestedCaps);
            return Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(caps);
        }

        #endregion
    }
}