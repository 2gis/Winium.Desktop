namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class TestWebDriver : RemoteWebDriver
    {
        #region Constants

        private const string GetDataGridCellCommand = "getDataGridCell";

        #endregion

        #region Constructors and Destructors

        public TestWebDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
        }

        public TestWebDriver(ICapabilities desiredCapabilities)
            : base(desiredCapabilities)
        {
        }

        public TestWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
            CommandInfoRepository.Instance.TryAddCommand(
                GetDataGridCellCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/cell/{row}/{column}"));
        }

        public TestWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        #endregion

        #region Public Methods and Operators

        public RemoteWebElement GetDataGridCell(IWebElement element, int row, int column)
        {
            var elementId =
                element.GetType()
                    .GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty)
                    .GetValue(element, null)
                    .ToString();

            var response = this.Execute(
                GetDataGridCellCommand,
                new Dictionary<string, object> { { "id", elementId }, { "row", row }, { "column", column } });

            var elementDictionary = response.Value as Dictionary<string, object>;
            if (elementDictionary == null)
            {
                return null;
            }

            return this.CreateElement((string)elementDictionary["ELEMENT"]);
        }

        #endregion
    }

    public class GetDataGridCellTests
    {
        #region Public Properties

        public TestWebDriver Driver { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void GetDataGridCellAndCheckValue()
        {
            var mainWindow = this.Driver.FindElementById("WpfTestApplicationMainWindow");
            var tab = mainWindow.FindElement(By.Name("TabItem4"));
            tab.Click();

            var dataGrid = tab.FindElement(By.Id("DataGrid"));

            var dataGridCell = this.Driver.GetDataGridCell(dataGrid, 0, 1);

            Assert.AreEqual("one", dataGridCell.Text);
        }

        [SetUp]
        public void SetUp()
        {
            var dc = new DesiredCapabilities();
            dc.SetCapability("app", Path.Combine(Environment.CurrentDirectory, "WpfTestApplication.exe"));
            this.Driver = new TestWebDriver(new Uri("http://localhost:9999"), dc);
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Close();
        }

        #endregion
    }
}
