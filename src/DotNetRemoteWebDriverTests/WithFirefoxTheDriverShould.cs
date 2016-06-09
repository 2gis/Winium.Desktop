﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class WithFirefoxTheDriverShould : RemoteDriverInstanceBaseTest
    {
        private RemoteWebDriver _driver;

        [TestCleanup]
        public override void Cleanup()
        {
            _driver?.Quit();
            _driver?.Dispose();

            base.Cleanup();
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Refresh()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.Firefox();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);
            _driver.Navigate().GoToUrl("http://google.com");
            _driver.Navigate().Refresh();
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Get_Title()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.Firefox();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);
            _driver.Navigate().GoToUrl("http://google.com");

            Assert.AreEqual("Google", _driver.Title);
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Maximize_Window()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.InternetExplorer();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);
            _driver.Navigate().GoToUrl("http://google.com");
            _driver.Manage().Window.Maximize();
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Go_Back()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.InternetExplorer();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);
            _driver.Navigate().GoToUrl("http://google.com");
            _driver.FindElement(By.LinkText("About")).Click();
            _driver.Navigate().Back();
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Open_Google_And_Search()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.Firefox();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);

            _driver.Navigate().GoToUrl("http://output.jsbin.com/bobiba/");
            var label = _driver.FindElement(By.Id("joanna"));
            Assert.AreEqual("Simple text", label.Text);

            var input = _driver.FindElement(By.Name("luna"));
            input.SendKeys("some text");

            _driver.FindElement(By.LinkText("Goto google")).Click();

            var inputs = _driver.FindElements(By.TagName("input"));
            var searchButton = inputs.Single(i => i.GetAttribute("name") == "btnK");
            Assert.AreEqual("Google Search", searchButton.GetAttribute("value"));
        }

        [TestMethod, TestCategory("Integration")]
        public void Be_Able_To_Return_Url()
        {
            var remoteUrl = new Uri("http://localhost:4444/");
            var capabilities = DesiredCapabilities.Firefox();
            _driver = new RemoteWebDriver(remoteUrl, capabilities);

            var url = "http://output.jsbin.com/bobiba/";
            _driver.Navigate().GoToUrl(url);
            Assert.AreEqual(url, _driver.Url);
        }
    }
}
