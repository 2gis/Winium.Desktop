using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotNetRemoteWebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class TheDriverShould
    {
        private Process _driverProcess;

        [TestMethod, TestCategory("Integration")]
        public void Can_Be_Started_After_Being_Killed()
        {
            _driverProcess = Process.Start("DotNetRemoteWebDriver.exe", "--log-path driver.log");
            Assert.IsNotNull(_driverProcess);
            LaunchDriver(DesiredCapabilities.Firefox(), "http://google.com");
            LaunchDriver(DesiredCapabilities.Chrome(), "http://google.com");
            LaunchDriver(DesiredCapabilities.InternetExplorer(), "http://google.com");

            _driverProcess.CloseMainWindow();
            _driverProcess.KillAndWait();

            _driverProcess = Process.Start("DotNetRemoteWebDriver.exe", "--log-path driver.log");
            Assert.IsNotNull(_driverProcess);
            Assert.IsFalse(_driverProcess.HasExited);
            _driverProcess.KillAndWait();
        }
        
        private void LaunchDriver(DesiredCapabilities browserCapabilities, string url)
        {
            var host = new Uri("http://localhost:4444/");
            var driver = new RemoteWebDriver(host, browserCapabilities);
            driver.Navigate().GoToUrl(url);
        }
    }
}
