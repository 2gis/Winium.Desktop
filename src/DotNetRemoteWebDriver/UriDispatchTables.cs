using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotNetRemoteWebDriver
{
    internal class UriDispatchTables
    {
        #region Constructors and Destructors

        public UriDispatchTables(Uri prefix)
        {
            InitializeSeleniumCommandDictionary();
            InitializeWiniumCommandDictionary();
            ConstructDispatcherTables(prefix);
        }

        #endregion

        #region Public Methods and Operators

        public UriTemplateMatch Match(string httpMethod, Uri uriToMatch)
        {
            var table = FindDispatcherTable(httpMethod);
            return table?.MatchSingle(uriToMatch);
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, CommandInfo> _commandDictionary = new Dictionary<string, CommandInfo>();

        private UriTemplateTable _deleteDispatcherTable;

        private UriTemplateTable _getDispatcherTable;

        private UriTemplateTable _postDispatcherTable;

        #endregion

        #region Methods

        internal UriTemplateTable FindDispatcherTable(string httpMethod)
        {
            UriTemplateTable tableToReturn = null;
            switch (httpMethod)
            {
                case CommandInfo.GetCommand:
                    tableToReturn = _getDispatcherTable;
                    break;

                case CommandInfo.PostCommand:
                    tableToReturn = _postDispatcherTable;
                    break;

                case CommandInfo.DeleteCommand:
                    tableToReturn = _deleteDispatcherTable;
                    break;
            }

            return tableToReturn;
        }

        private void ConstructDispatcherTables(Uri prefix)
        {
            _getDispatcherTable = new UriTemplateTable(prefix);
            _postDispatcherTable = new UriTemplateTable(prefix);
            _deleteDispatcherTable = new UriTemplateTable(prefix);

            var fields = typeof (DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var commandName = field.GetValue(null).ToString();
                var commandInformation = _commandDictionary[commandName];
                var commandUriTemplate = new UriTemplate(commandInformation.ResourcePath);
                var templateTable = FindDispatcherTable(commandInformation.Method);
                templateTable.KeyValuePairs.Add(new KeyValuePair<UriTemplate, object>(commandUriTemplate, commandName));
            }

            _getDispatcherTable.MakeReadOnly(false);
            _postDispatcherTable.MakeReadOnly(false);
            _deleteDispatcherTable.MakeReadOnly(false);
        }

        private void InitializeSeleniumCommandDictionary()
        {
            _commandDictionary.Add(DriverCommand.DefineDriverMapping, new CommandInfo("POST", "/config/drivers"));
            _commandDictionary.Add(DriverCommand.Status, new CommandInfo("GET", "/status"));
            _commandDictionary.Add(DriverCommand.NewSession, new CommandInfo("POST", "/session"));
            _commandDictionary.Add(DriverCommand.GetSessionList, new CommandInfo("GET", "/sessions"));
            _commandDictionary.Add(
                DriverCommand.GetSessionCapabilities,
                new CommandInfo("GET", "/session/{sessionId}"));
            _commandDictionary.Add(DriverCommand.Quit, new CommandInfo("DELETE", "/session/{sessionId}"));
            _commandDictionary.Add(
                DriverCommand.GetCurrentWindowHandle,
                new CommandInfo("GET", "/session/{sessionId}/window_handle"));
            _commandDictionary.Add(
                DriverCommand.GetWindowHandles,
                new CommandInfo("GET", "/session/{sessionId}/window_handles"));
            _commandDictionary.Add(DriverCommand.GetCurrentUrl, new CommandInfo("GET", "/session/{sessionId}/url"));
            _commandDictionary.Add(DriverCommand.Get, new CommandInfo("POST", "/session/{sessionId}/url"));
            _commandDictionary.Add(DriverCommand.GoForward, new CommandInfo("POST", "/session/{sessionId}/forward"));
            _commandDictionary.Add(DriverCommand.GoBack, new CommandInfo("POST", "/session/{sessionId}/back"));
            _commandDictionary.Add(DriverCommand.Refresh, new CommandInfo("POST", "/session/{sessionId}/refresh"));
            _commandDictionary.Add(
                DriverCommand.ExecuteScript,
                new CommandInfo("POST", "/session/{sessionId}/execute"));
            _commandDictionary.Add(
                DriverCommand.ExecuteAsyncScript,
                new CommandInfo("POST", "/session/{sessionId}/execute_async"));
            _commandDictionary.Add(
                DriverCommand.Screenshot,
                new CommandInfo("GET", "/session/{sessionId}/screenshot"));
            _commandDictionary.Add(
                DriverCommand.SwitchToFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame"));
            _commandDictionary.Add(
                DriverCommand.SwitchToParentFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame/parent"));
            _commandDictionary.Add(
                DriverCommand.SwitchToWindow,
                new CommandInfo("POST", "/session/{sessionId}/window"));
            _commandDictionary.Add(
                DriverCommand.GetAllCookies,
                new CommandInfo("GET", "/session/{sessionId}/cookie"));
            _commandDictionary.Add(DriverCommand.AddCookie, new CommandInfo("POST", "/session/{sessionId}/cookie"));
            _commandDictionary.Add(
                DriverCommand.DeleteAllCookies,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie"));
            _commandDictionary.Add(
                DriverCommand.DeleteCookie,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie/{name}"));
            _commandDictionary.Add(
                DriverCommand.GetPageSource,
                new CommandInfo("GET", "/session/{sessionId}/source"));
            _commandDictionary.Add(DriverCommand.GetTitle, new CommandInfo("GET", "/session/{sessionId}/title"));
            _commandDictionary.Add(
                DriverCommand.FindElement,
                new CommandInfo("POST", "/session/{sessionId}/element"));
            _commandDictionary.Add(
                DriverCommand.FindElements,
                new CommandInfo("POST", "/session/{sessionId}/elements"));
            _commandDictionary.Add(
                DriverCommand.GetActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/element/active"));
            _commandDictionary.Add(
                DriverCommand.FindChildElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/element"));
            _commandDictionary.Add(
                DriverCommand.FindChildElements,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/elements"));
            _commandDictionary.Add(
                DriverCommand.DescribeElement,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}"));
            _commandDictionary.Add(
                DriverCommand.ClickElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/click"));
            _commandDictionary.Add(
                DriverCommand.GetElementText,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/text"));
            _commandDictionary.Add(
                DriverCommand.SubmitElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/submit"));
            _commandDictionary.Add(
                DriverCommand.SendKeysToElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/value"));
            _commandDictionary.Add(
                DriverCommand.GetElementTagName,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/name"));
            _commandDictionary.Add(
                DriverCommand.ClearElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/clear"));
            _commandDictionary.Add(
                DriverCommand.IsElementSelected,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/selected"));
            _commandDictionary.Add(
                DriverCommand.IsElementEnabled,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/enabled"));
            _commandDictionary.Add(
                DriverCommand.IsElementDisplayed,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/displayed"));
            _commandDictionary.Add(
                DriverCommand.GetElementLocation,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location"));
            _commandDictionary.Add(
                DriverCommand.GetElementLocationOnceScrolledIntoView,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location_in_view"));
            _commandDictionary.Add(
                DriverCommand.GetElementSize,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/size"));
            _commandDictionary.Add(
                DriverCommand.GetElementValueOfCssProperty,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/css/{propertyName}"));
            _commandDictionary.Add(
                DriverCommand.GetElementAttribute,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/attribute/{name}"));
            _commandDictionary.Add(
                DriverCommand.ElementEquals,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/equals/{other}"));
            _commandDictionary.Add(DriverCommand.Close, new CommandInfo("DELETE", "/session/{sessionId}/window"));
            _commandDictionary.Add(
                DriverCommand.GetWindowSize,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/size"));
            _commandDictionary.Add(
                DriverCommand.SetWindowSize,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/size"));
            _commandDictionary.Add(
                DriverCommand.GetWindowPosition,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/position"));
            _commandDictionary.Add(
                DriverCommand.SetWindowPosition,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/position"));
            _commandDictionary.Add(
                DriverCommand.MaximizeWindow,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/maximize"));
            _commandDictionary.Add(
                DriverCommand.GetOrientation,
                new CommandInfo("GET", "/session/{sessionId}/orientation"));
            _commandDictionary.Add(
                DriverCommand.SetOrientation,
                new CommandInfo("POST", "/session/{sessionId}/orientation"));
            _commandDictionary.Add(
                DriverCommand.DismissAlert,
                new CommandInfo("POST", "/session/{sessionId}/dismiss_alert"));
            _commandDictionary.Add(
                DriverCommand.AcceptAlert,
                new CommandInfo("POST", "/session/{sessionId}/accept_alert"));
            _commandDictionary.Add(
                DriverCommand.GetAlertText,
                new CommandInfo("GET", "/session/{sessionId}/alert_text"));
            _commandDictionary.Add(
                DriverCommand.SetAlertValue,
                new CommandInfo("POST", "/session/{sessionId}/alert_text"));
            _commandDictionary.Add(
                DriverCommand.SetTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts"));
            _commandDictionary.Add(
                DriverCommand.ImplicitlyWait,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/implicit_wait"));
            _commandDictionary.Add(
                DriverCommand.SetAsyncScriptTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/async_script"));
            _commandDictionary.Add(DriverCommand.MouseClick, new CommandInfo("POST", "/session/{sessionId}/click"));
            _commandDictionary.Add(
                DriverCommand.MouseDoubleClick,
                new CommandInfo("POST", "/session/{sessionId}/doubleclick"));
            _commandDictionary.Add(
                DriverCommand.MouseDown,
                new CommandInfo("POST", "/session/{sessionId}/buttondown"));
            _commandDictionary.Add(DriverCommand.MouseUp, new CommandInfo("POST", "/session/{sessionId}/buttonup"));
            _commandDictionary.Add(
                DriverCommand.MouseMoveTo,
                new CommandInfo("POST", "/session/{sessionId}/moveto"));
            _commandDictionary.Add(
                DriverCommand.SendKeysToActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/keys"));
            _commandDictionary.Add(
                DriverCommand.TouchSingleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/click"));
            _commandDictionary.Add(
                DriverCommand.TouchPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/down"));
            _commandDictionary.Add(
                DriverCommand.TouchRelease,
                new CommandInfo("POST", "/session/{sessionId}/touch/up"));
            _commandDictionary.Add(
                DriverCommand.TouchMove,
                new CommandInfo("POST", "/session/{sessionId}/touch/move"));
            _commandDictionary.Add(
                DriverCommand.TouchScroll,
                new CommandInfo("POST", "/session/{sessionId}/touch/scroll"));
            _commandDictionary.Add(
                DriverCommand.TouchDoubleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/doubleclick"));
            _commandDictionary.Add(
                DriverCommand.TouchLongPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/longclick"));
            _commandDictionary.Add(
                DriverCommand.TouchFlick,
                new CommandInfo("POST", "/session/{sessionId}/touch/flick"));
            _commandDictionary.Add(DriverCommand.UploadFile, new CommandInfo("POST", "/session/{sessionId}/file"));
        }

        private void InitializeWiniumCommandDictionary()
        {
            _commandDictionary.Add(
                DriverCommand.FindDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/cell/{row}/{column}"));

            _commandDictionary.Add(
                DriverCommand.GetDataGridColumnCount,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/column/count"));

            _commandDictionary.Add(
                DriverCommand.GetDataGridRowCount,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/row/count"));

            _commandDictionary.Add(
                DriverCommand.ScrollToDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/scroll/{row}/{column}"));

            _commandDictionary.Add(
                DriverCommand.SelectDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/select/{row}/{column}"));

            _commandDictionary.Add(
                DriverCommand.IsComboBoxExpanded,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expanded"));

            _commandDictionary.Add(
                DriverCommand.ExpandComboBox,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expand"));

            _commandDictionary.Add(
                DriverCommand.CollapseComboBox,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/collapse"));

            _commandDictionary.Add(
                DriverCommand.FindComboBoxSelectedItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/items/selected"));

            _commandDictionary.Add(
                DriverCommand.ScrollToComboBoxItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/scroll"));

            _commandDictionary.Add(
                DriverCommand.ScrollToListBoxItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/listbox/scroll"));

            _commandDictionary.Add(
                DriverCommand.FindMenuItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/item/{path}"));

            _commandDictionary.Add(
                DriverCommand.SelectMenuItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/select/{path}"));
        }

        #endregion
    }
}