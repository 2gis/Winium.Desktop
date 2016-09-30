namespace WpfTestApplication.Tests
{
    #region using

    using System;
    using System.Collections.Generic;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class TestWebDriver : RemoteWebDriver
    {
        #region Constants

        private const string CollapseComboBoxCommand = "collapseComboBox";

        private const string ExpandComboBoxCommand = "expandComboBox";

        private const string FindComboBoxSelctedItemCommand = "getComboBoxSelctedItem";

        private const string FindDataGridCellCommand = "findDataGridCell";

        private const string FindMenuItemCommand = "getMenuItemCommand";

        private const string GetDataGridColumnCountCommand = "getDataGridColumnCount";

        private const string GetDataGridRowCountCommand = "getDataGridRowCount";

        private const string IsComboBoxExpandedCommand = "isComboBoxExpanded";

        private const string ScrollToDataGridCellCommand = "scrollToDataGridCell";

        private const string SelectDataGridCellCommand = "selectDataGridCell";

        private const string SelectMenuItemCommand = "selectMenuItem";

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
            /*CommandInfoRepository.Instance.TryAddCommand(
                FindDataGridCellCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/cell/{row}/{column}"));

            CommandInfoRepository.Instance.TryAddCommand(
                GetDataGridColumnCountCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/column/count"));

            CommandInfoRepository.Instance.TryAddCommand(
                GetDataGridRowCountCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/row/count"));

            CommandInfoRepository.Instance.TryAddCommand(
                ScrollToDataGridCellCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/scroll/{row}/{column}"));

            CommandInfoRepository.Instance.TryAddCommand(
                SelectDataGridCellCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/select/{row}/{column}"));

            CommandInfoRepository.Instance.TryAddCommand(
                IsComboBoxExpandedCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expanded"));

            CommandInfoRepository.Instance.TryAddCommand(
                ExpandComboBoxCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expand"));

            CommandInfoRepository.Instance.TryAddCommand(
                CollapseComboBoxCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/collapse"));

            CommandInfoRepository.Instance.TryAddCommand(
                FindComboBoxSelctedItemCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/items/selected"));

            CommandInfoRepository.Instance.TryAddCommand(
                FindMenuItemCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/item/{path}"));

            CommandInfoRepository.Instance.TryAddCommand(
                SelectMenuItemCommand,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/select/{path}"));*/
        }

        public TestWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        #endregion

        #region Public Methods and Operators

        public void CollapseComboBox(IWebElement element)
        {
            var elementId = TestHelper.GetElementId(element);

            this.Execute(CollapseComboBoxCommand, new Dictionary<string, object> { { "id", elementId } });
        }

        public void ExpandComboBox(IWebElement element)
        {
            var elementId = TestHelper.GetElementId(element);

            this.Execute(ExpandComboBoxCommand, new Dictionary<string, object> { { "id", elementId } });
        }

        public RemoteWebElement FindComboBoxSelctedItem(IWebElement element)
        {
            var elementId = TestHelper.GetElementId(element);

            var response = this.Execute(
                FindComboBoxSelctedItemCommand,
                new Dictionary<string, object> { { "id", elementId } });

            return this.CreateElementFromResponse(response);
        }

        public RemoteWebElement FindDataGridCell(IWebElement element, int row, int column)
        {
            var elementId = TestHelper.GetElementId(element);

            var response = this.Execute(
                FindDataGridCellCommand,
                new Dictionary<string, object> { { "id", elementId }, { "row", row }, { "column", column } });

            return this.CreateElementFromResponse(response);
        }

        public RemoteWebElement FindMenuItem(IWebElement element, string path)
        {
            var response = this.Execute(
                FindMenuItemCommand,
                new Dictionary<string, object> { { "id", TestHelper.GetElementId(element) }, { "path", path } });

            return this.CreateElementFromResponse(response);
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

        public bool IsComboBoxExpanded(IWebElement element)
        {
            var elementId = TestHelper.GetElementId(element);

            var response = this.Execute(
                IsComboBoxExpandedCommand,
                new Dictionary<string, object> { { "id", elementId } });

            return bool.Parse(response.Value.ToString());
        }

        public void ScrollToDataGridCell(IWebElement element, int row, int column)
        {
            var elementId = TestHelper.GetElementId(element);

            this.Execute(
                ScrollToDataGridCellCommand,
                new Dictionary<string, object> { { "id", elementId }, { "row", row }, { "column", column } });
        }

        public void SelectDataGridCell(IWebElement element, int row, int column)
        {
            var elementId = TestHelper.GetElementId(element);

            this.Execute(
                SelectDataGridCellCommand,
                new Dictionary<string, object> { { "id", elementId }, { "row", row }, { "column", column } });
        }

        public void SelectMenuItem(IWebElement element, string path)
        {
            this.Execute(
                SelectMenuItemCommand,
                new Dictionary<string, object> { { "id", TestHelper.GetElementId(element) }, { "path", path } });
        }

        #endregion

        #region Methods

        private RemoteWebElement CreateElementFromResponse(Response response)
        {
            var elementDictionary = response.Value as Dictionary<string, object>;
            if (elementDictionary == null)
            {
                return null;
            }

            return this.CreateElement((string)elementDictionary["ELEMENT"]);
        }

        #endregion
    }
}
