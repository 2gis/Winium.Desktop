using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class WithFirefoxTheDriverShould : RemoteDriverInstanceBaseTest
    {
        private RemoteWebDriver _driver;

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Open_Google_And_Search()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.Firefox();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);

            _driver.Navigate().GoToUrl("http://google.com");
            var searchElement = _driver.FindElement(By.Name("q"));
            searchElement.SendKeys("semla");
            searchElement.Submit();

            var hits = _driver.FindElements(By.TagName("h3"));
            var firstHit = hits.First().Text;
            Assert.IsTrue(firstHit.ToLower().Contains("semla"));
        }
    }
}
