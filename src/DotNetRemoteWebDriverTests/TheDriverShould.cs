using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly string[] _processNames = new[] {"chromedriver", "iedriverserver", "wires", "chrome", "firefox", "iexplore"};
        private Process _driverProcess;

        [TestMethod, TestCategory("Integration")]
        public void Can_Be_Started_After_Being_Killed()
        {
            var timeBeforeTest = new DateTime();
            try
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
            finally
            {
                var killProcesses = Process.GetProcesses()
                    .Where(p => _processNames.Contains(p.ProcessName.ToLower()))
                    .Where(p => p.StartTime > timeBeforeTest)
                    .ToArray();

                foreach (var process in killProcesses)
                {
                    try
                    {
                        process.KillAndWait();
                    }
                    catch (Win32Exception)
                    {
                        Console.WriteLine($"So I couldn't kill this one process, {process.Id}, but that's fine..");
                    }
                }
            }
        }

        private void LaunchDriver(DesiredCapabilities browserCapabilities, string url)
        {
            var host = new Uri("http://localhost:4444/");
            var driver = new RemoteWebDriver(host, browserCapabilities);
            driver.Navigate().GoToUrl(url);
        }
    }
}
