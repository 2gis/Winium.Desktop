using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class InternetExplorerDriverShould : RemoteDriverInstanceBaseTest
    {
        private RemoteWebDriver _driver;

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Start_With_Capabilities()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.InternetExplorer();
            capabilities.SetCapability("browserAttachTimeout", 15000);
            using (_driver = new RemoteWebDriver(remoteUrl, capabilities))
                _driver.Navigate().GoToUrl("http://google.com");
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Return_Capabilities()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.InternetExplorer();
            capabilities.SetCapability("browserAttachTimeout", 15000);
            using (_driver = new RemoteWebDriver(remoteUrl, capabilities))
                Assert.AreEqual("internet explorer", _driver.Capabilities.BrowserName);
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Return_Window_Handle()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.InternetExplorer();
            using (_driver = new RemoteWebDriver(remoteUrl, capabilities))
            {
                _driver.Navigate().GoToUrl("http://google.com");
                Assert.IsNotNull(_driver.CurrentWindowHandle);
            }
        }
    }
}