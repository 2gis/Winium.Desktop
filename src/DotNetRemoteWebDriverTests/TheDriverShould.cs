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
        private string[] _trackProcesses = new[]
        {
            "firefox", "chrome", "iexplore", "dotnetremotewebdriver",
            "wires", "iedriverserver", "chromedriver"
        };

        private Process _driverProcess;

        [TestMethod, TestCategory("Integration")]
        public void Close_Down_Drivers_On_Exit()
        {
            var runningBefore = FindRunningProcesses();

            _driverProcess = Process.Start("DotNetRemoteWebDriver.exe");
            Assert.IsNotNull(_driverProcess);
            LaunchDriver(DesiredCapabilities.Firefox(), "http://google.com");
            LaunchDriver(DesiredCapabilities.Chrome(), "http://google.com");
            LaunchDriver(DesiredCapabilities.InternetExplorer(), "http://google.com");

            _driverProcess.CloseMainWindow();
            if (!_driverProcess.WaitForExit(1000))
                throw new Exception("Failed to kill driver process");

            var runningAfter = FindRunningProcesses();
            var newProcesses = runningAfter.Where(id => !runningBefore.Contains(id));
            var stillRunning = WaitUntilClosed(newProcesses).ToArray();
            if (stillRunning.Length > 0)
                Assert.Fail("Processes should've been closed: " + string.Join(", ", stillRunning));
        }

        private IEnumerable<string> WaitUntilClosed(IEnumerable<int> running)
        {
            var closed = new List<string>();
            foreach (var processId in running)
            {
                try
                {
                    var process = Process.GetProcessById(processId);
                    if (!process.WaitForExit(1000))
                        closed.Add(process.ProcessName);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Failed to get {processId}: " + e.Message);
                }
            }
            return closed;
        }

        private int[] FindRunningProcesses()
        {
            return Process.GetProcesses()
                .Where(p => _trackProcesses.Contains(p.ProcessName.ToLower()))
                .Select(p => p.Id)
                .ToArray();
        }

        private void LaunchDriver(DesiredCapabilities browserCapabilities, string url)
        {
            var host = new Uri("http://localhost:4444/");
            var driver = new RemoteWebDriver(host, browserCapabilities);
            driver.Navigate().GoToUrl(url);
        }
    }
}
