namespace DotNetRemoteWebDriver.CommandExecutors
{
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

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            // So this method should instantiate a driver of the given browser type and return
            // the session id plus capabilities
            var capabilities = this.ExecutedCommand.Parameters["desiredCapabilities"];
            var driver = capabilities["browserName"].ToString().ToLower();

            var serializedCaps = JsonConvert.SerializeObject(capabilities);
            object actualCapabilities = null;
            switch (driver)
            {
                case "internet explorer":
                    var ieOptions = JsonConvert.DeserializeObject<InternetExplorerOptions>(serializedCaps);
                    actualCapabilities = ieOptions; 
                    this.Automator.Driver = new InternetExplorerDriver(ieOptions);
                    break;
                case "chrome":
                    var chromeOptions = JsonConvert.DeserializeObject<ChromeOptions>(serializedCaps);
                    actualCapabilities = chromeOptions;
                    this.Automator.Driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefox":
                    var ffOptions = JsonConvert.DeserializeObject<FirefoxOptions>(serializedCaps);
                    actualCapabilities = ffOptions;
                    var firefoxBinary = new FirefoxBinary(@"c:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    this.Automator.Driver = new FirefoxDriver(firefoxBinary, new FirefoxProfile());
                    break;
                default:
                    throw new NotSupportedException("Driver is invalid or not supported: " + driver);
            }

            return this.JsonResponse(ResponseStatus.Success, actualCapabilities);
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;

            this.Automator.Application = new Application(appPath);
            if (!debugDoNotDeploy)
            {
                this.Automator.Application.Start(appArguments);
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            this.Automator.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }

        private Capabilities ParseCapabilities()
        {
            var requestedCaps = this.ExecutedCommand.Parameters["desiredCapabilities"];
            var caps = JsonConvert.SerializeObject(requestedCaps);
            return this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(caps);
        }

        #endregion
    }
}
