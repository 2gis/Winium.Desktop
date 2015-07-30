namespace WpfTestApplication.Tests.AdditionalCommandTests
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class TestWebDriver : RemoteWebDriver
    {
        #region Constants

        private const string GetDataGridCellCommand = "getDataGridCell";

        private const string GetDataGridColumnCountCommand = "getDataGridColumnCount";

        private const string GetDataGridRowCountCommand = "getDataGridRowCount";

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
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/cell/{row}/{column}"));

            CommandInfoRepository.Instance.TryAddCommand(
                GetDataGridColumnCountCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/column/count"));

            CommandInfoRepository.Instance.TryAddCommand(
                GetDataGridRowCountCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/row/count"));
        }

        public TestWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        #endregion

        #region Public Methods and Operators

        public RemoteWebElement GetDataGridCell(IWebElement element, int row, int column)
        {
            var elementId = TestHelper.GetElementId(element);

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

        public int GetDataGridColumnCount(IWebElement element)
        {
            var elementId = TestHelper.GetElementId(element);

            var response = this.Execute(
                GetDataGridColumnCountCommand,
                new Dictionary<string, object> { { "id", elementId } });

            return int.Parse(response.Value.ToString());
        }

        public int GetDataGridRowCount(IWebElement element)
        {
            var elementId = TestHelper.GetElementId(element);

            var response = this.Execute(
                GetDataGridRowCountCommand,
                new Dictionary<string, object> { { "id", elementId } });

            return int.Parse(response.Value.ToString());
        }

        #endregion
    }

    public static class TestHelper
    {
        #region Public Methods and Operators

        public static string GetElementId(IWebElement element)
        {
            return
                element.GetType()
                    .GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty)
                    .GetValue(element, null)
                    .ToString();
        }

        #endregion
    }
}
