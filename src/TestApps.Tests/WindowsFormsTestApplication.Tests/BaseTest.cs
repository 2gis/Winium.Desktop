namespace WindowsFormsTestApplication.Tests
{
    #region using

    using System;
    using System.IO;

    using NUnit.Framework;

    using OpenQA.Selenium.Remote;

    #endregion

    public class BaseTest
    {
        #region Public Properties

        public RemoteWebDriver Driver { get; set; }

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void SetUp()
        {
            var dc = new DesiredCapabilities();
            dc.SetCapability("app", Path.Combine(Environment.CurrentDirectory, "WindowsFormsTestApplication.exe"));
            dc.SetCapability("launchDelay", 2);
            this.Driver = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Close();
        }

        #endregion
    }
}
