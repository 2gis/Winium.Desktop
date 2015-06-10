namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using System;
    using System.IO;
    using System.Linq;

    using WindowsInput.Native;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Remote;

    #endregion

    [TestFixture]
    public class ClickWithKeysTests
    {
        #region Public Properties

        public RemoteWebDriver Driver { get; set; }

        public IWebElement MainWindow { get; set; }

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void SetUp()
        {
            var dc = new DesiredCapabilities();
            dc.SetCapability("app", Path.Combine(Environment.CurrentDirectory, "SomeTestApp.exe"));
            this.Driver = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);
            this.MainWindow = this.Driver.FindElementById("SomeTestApp");
        }

        [Test]
        public void SelectItemsInListFromFirstToRandom()
        {
            var list = this.MainWindow.FindElement(By.Id("List"));
            var items = list.FindElements(By.ClassName(""));
            var first = items.First();
            var randomId = new Random().Next(1, items.Count);
            var random = items.ElementAt(randomId);
            var actions = new Actions(this.Driver);

            actions.Click(first).KeyDown(Keys.Shift).Click(random).KeyUp(Keys.Shift).Perform();

            var selectedItemsCount = list.FindElements(By.ClassName("")).Count(item => item.Selected);
            
            Assert.AreEqual(randomId + 1, selectedItemsCount);
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Close();
        }

        #endregion
    }
}
