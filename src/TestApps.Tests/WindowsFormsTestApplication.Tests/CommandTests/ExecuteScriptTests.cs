namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using System;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    [TestFixture]
    public class ExecuteScriptTests
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
            var random = items.ElementAt(new Random().Next(1, items.Count));

            first.Click();
            this.Driver.ExecuteScript("input: shift+click", random);

            // Надо бы таки проверять выделение элементов.
            //Assert.AreEqual("CARAMBA", this.MainWindow.FindElement(By.Id("TextBox1")).Text);
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Close();
        }

        #endregion
    }
}
