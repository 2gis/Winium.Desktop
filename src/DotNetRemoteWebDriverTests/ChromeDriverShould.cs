using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class ChromeDriverShould : RemoteDriverInstanceBaseTest
    {
        private RemoteWebDriver _driver;

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Start_With_Capabilities()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.Chrome();
            capabilities.SetCapability("args", new [] { "enable-logging", "v=1" });
            using (_driver = new RemoteWebDriver(remoteUrl, capabilities))
                _driver.Navigate().GoToUrl("http://google.com");
        }
    }
}
