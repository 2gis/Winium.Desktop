namespace Winium.Desktop.Driver
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Winium.StoreApps.Common;

    #endregion

    internal class UriDispatchTables
    {
        #region Fields

        private readonly Dictionary<string, CommandInfo> commandDictionary = new Dictionary<string, CommandInfo>();

        private UriTemplateTable deleteDispatcherTable;

        private UriTemplateTable getDispatcherTable;

        private UriTemplateTable postDispatcherTable;

        #endregion

        #region Constructors and Destructors

        public UriDispatchTables(Uri prefix)
        {
            this.InitializeSeleniumCommandDictionary();
            this.InitializeWiniumCommandDictionary();
            this.ConstructDispatcherTables(prefix);
        }

        #endregion

        #region Public Methods and Operators

        public UriTemplateMatch Match(string httpMethod, Uri uriToMatch)
        {
            var table = this.FindDispatcherTable(httpMethod);
            return table != null ? table.MatchSingle(uriToMatch) : null;
        }

        #endregion

        #region Methods

        internal UriTemplateTable FindDispatcherTable(string httpMethod)
        {
            UriTemplateTable tableToReturn = null;
            switch (httpMethod)
            {
                case CommandInfo.GetCommand:
                    tableToReturn = this.getDispatcherTable;
                    break;

                case CommandInfo.PostCommand:
                    tableToReturn = this.postDispatcherTable;
                    break;

                case CommandInfo.DeleteCommand:
                    tableToReturn = this.deleteDispatcherTable;
                    break;
            }

            return tableToReturn;
        }

        private void ConstructDispatcherTables(Uri prefix)
        {
            this.getDispatcherTable = new UriTemplateTable(prefix);
            this.postDispatcherTable = new UriTemplateTable(prefix);
            this.deleteDispatcherTable = new UriTemplateTable(prefix);

            var fields = typeof(DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var commandName = field.GetValue(null).ToString();
                var commandInformation = this.commandDictionary[commandName];
                var commandUriTemplate = new UriTemplate(commandInformation.ResourcePath);
                var templateTable = this.FindDispatcherTable(commandInformation.Method);
                templateTable.KeyValuePairs.Add(new KeyValuePair<UriTemplate, object>(commandUriTemplate, commandName));
            }

            this.getDispatcherTable.MakeReadOnly(false);
            this.postDispatcherTable.MakeReadOnly(false);
            this.deleteDispatcherTable.MakeReadOnly(false);
        }

        private void InitializeSeleniumCommandDictionary()
        {
            this.commandDictionary.Add(DriverCommand.DefineDriverMapping, new CommandInfo("POST", "/config/drivers"));
            this.commandDictionary.Add(DriverCommand.Status, new CommandInfo("GET", "/status"));
            this.commandDictionary.Add(DriverCommand.NewSession, new CommandInfo("POST", "/session"));
            this.commandDictionary.Add(DriverCommand.GetSessionList, new CommandInfo("GET", "/sessions"));
            this.commandDictionary.Add(
                DriverCommand.GetSessionCapabilities,
                new CommandInfo("GET", "/session/{sessionId}"));
            this.commandDictionary.Add(DriverCommand.Quit, new CommandInfo("DELETE", "/session/{sessionId}"));
            this.commandDictionary.Add(
                DriverCommand.GetCurrentWindowHandle,
                new CommandInfo("GET", "/session/{sessionId}/window_handle"));
            this.commandDictionary.Add(
                DriverCommand.GetWindowHandles,
                new CommandInfo("GET", "/session/{sessionId}/window_handles"));
            this.commandDictionary.Add(DriverCommand.GetCurrentUrl, new CommandInfo("GET", "/session/{sessionId}/url"));
            this.commandDictionary.Add(DriverCommand.Get, new CommandInfo("POST", "/session/{sessionId}/url"));
            this.commandDictionary.Add(DriverCommand.GoForward, new CommandInfo("POST", "/session/{sessionId}/forward"));
            this.commandDictionary.Add(DriverCommand.GoBack, new CommandInfo("POST", "/session/{sessionId}/back"));
            this.commandDictionary.Add(DriverCommand.Refresh, new CommandInfo("POST", "/session/{sessionId}/refresh"));
            this.commandDictionary.Add(
                DriverCommand.ExecuteScript,
                new CommandInfo("POST", "/session/{sessionId}/execute"));
            this.commandDictionary.Add(
                DriverCommand.ExecuteAsyncScript,
                new CommandInfo("POST", "/session/{sessionId}/execute_async"));
            this.commandDictionary.Add(
                DriverCommand.Screenshot,
                new CommandInfo("GET", "/session/{sessionId}/screenshot"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToParentFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame/parent"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToWindow,
                new CommandInfo("POST", "/session/{sessionId}/window"));
            this.commandDictionary.Add(
                DriverCommand.GetAllCookies,
                new CommandInfo("GET", "/session/{sessionId}/cookie"));
            this.commandDictionary.Add(DriverCommand.AddCookie, new CommandInfo("POST", "/session/{sessionId}/cookie"));
            this.commandDictionary.Add(
                DriverCommand.DeleteAllCookies,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie"));
            this.commandDictionary.Add(
                DriverCommand.DeleteCookie,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie/{name}"));
            this.commandDictionary.Add(
                DriverCommand.GetPageSource,
                new CommandInfo("GET", "/session/{sessionId}/source"));
            this.commandDictionary.Add(DriverCommand.GetTitle, new CommandInfo("GET", "/session/{sessionId}/title"));
            this.commandDictionary.Add(
                DriverCommand.FindElement,
                new CommandInfo("POST", "/session/{sessionId}/element"));
            this.commandDictionary.Add(
                DriverCommand.FindElements,
                new CommandInfo("POST", "/session/{sessionId}/elements"));
            this.commandDictionary.Add(
                DriverCommand.GetActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/element/active"));
            this.commandDictionary.Add(
                DriverCommand.FindChildElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/element"));
            this.commandDictionary.Add(
                DriverCommand.FindChildElements,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/elements"));
            this.commandDictionary.Add(
                DriverCommand.DescribeElement,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}"));
            this.commandDictionary.Add(
                DriverCommand.ClickElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/click"));
            this.commandDictionary.Add(
                DriverCommand.GetElementText,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/text"));
            this.commandDictionary.Add(
                DriverCommand.SubmitElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/submit"));
            this.commandDictionary.Add(
                DriverCommand.SendKeysToElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/value"));
            this.commandDictionary.Add(
                DriverCommand.GetElementTagName,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/name"));
            this.commandDictionary.Add(
                DriverCommand.ClearElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/clear"));
            this.commandDictionary.Add(
                DriverCommand.IsElementSelected,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/selected"));
            this.commandDictionary.Add(
                DriverCommand.IsElementEnabled,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/enabled"));
            this.commandDictionary.Add(
                DriverCommand.IsElementDisplayed,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/displayed"));
            this.commandDictionary.Add(
                DriverCommand.GetElementLocation,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location"));
            this.commandDictionary.Add(
                DriverCommand.GetElementLocationOnceScrolledIntoView,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location_in_view"));
            this.commandDictionary.Add(
                DriverCommand.GetElementSize,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/size"));
            this.commandDictionary.Add(
                DriverCommand.GetElementValueOfCssProperty,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/css/{propertyName}"));
            this.commandDictionary.Add(
                DriverCommand.GetElementAttribute,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/attribute/{name}"));
            this.commandDictionary.Add(
                DriverCommand.ElementEquals,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/equals/{other}"));
            this.commandDictionary.Add(DriverCommand.Close, new CommandInfo("DELETE", "/session/{sessionId}/window"));
            this.commandDictionary.Add(
                DriverCommand.GetWindowSize,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/size"));
            this.commandDictionary.Add(
                DriverCommand.SetWindowSize,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/size"));
            this.commandDictionary.Add(
                DriverCommand.GetWindowPosition,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/position"));
            this.commandDictionary.Add(
                DriverCommand.SetWindowPosition,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/position"));
            this.commandDictionary.Add(
                DriverCommand.MaximizeWindow,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/maximize"));
            this.commandDictionary.Add(
                DriverCommand.GetOrientation,
                new CommandInfo("GET", "/session/{sessionId}/orientation"));
            this.commandDictionary.Add(
                DriverCommand.SetOrientation,
                new CommandInfo("POST", "/session/{sessionId}/orientation"));
            this.commandDictionary.Add(
                DriverCommand.DismissAlert,
                new CommandInfo("POST", "/session/{sessionId}/dismiss_alert"));
            this.commandDictionary.Add(
                DriverCommand.AcceptAlert,
                new CommandInfo("POST", "/session/{sessionId}/accept_alert"));
            this.commandDictionary.Add(
                DriverCommand.GetAlertText,
                new CommandInfo("GET", "/session/{sessionId}/alert_text"));
            this.commandDictionary.Add(
                DriverCommand.SetAlertValue,
                new CommandInfo("POST", "/session/{sessionId}/alert_text"));
            this.commandDictionary.Add(
                DriverCommand.SetTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts"));
            this.commandDictionary.Add(
                DriverCommand.ImplicitlyWait,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/implicit_wait"));
            this.commandDictionary.Add(
                DriverCommand.SetAsyncScriptTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/async_script"));
            this.commandDictionary.Add(DriverCommand.MouseClick, new CommandInfo("POST", "/session/{sessionId}/click"));
            this.commandDictionary.Add(
                DriverCommand.MouseDoubleClick,
                new CommandInfo("POST", "/session/{sessionId}/doubleclick"));
            this.commandDictionary.Add(
                DriverCommand.MouseDown,
                new CommandInfo("POST", "/session/{sessionId}/buttondown"));
            this.commandDictionary.Add(DriverCommand.MouseUp, new CommandInfo("POST", "/session/{sessionId}/buttonup"));
            this.commandDictionary.Add(
                DriverCommand.MouseMoveTo,
                new CommandInfo("POST", "/session/{sessionId}/moveto"));
            this.commandDictionary.Add(
                DriverCommand.SendKeysToActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/keys"));
            this.commandDictionary.Add(
                DriverCommand.TouchSingleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/click"));
            this.commandDictionary.Add(
                DriverCommand.TouchPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/down"));
            this.commandDictionary.Add(
                DriverCommand.TouchRelease,
                new CommandInfo("POST", "/session/{sessionId}/touch/up"));
            this.commandDictionary.Add(
                DriverCommand.TouchMove,
                new CommandInfo("POST", "/session/{sessionId}/touch/move"));
            this.commandDictionary.Add(
                DriverCommand.TouchScroll,
                new CommandInfo("POST", "/session/{sessionId}/touch/scroll"));
            this.commandDictionary.Add(
                DriverCommand.TouchDoubleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/doubleclick"));
            this.commandDictionary.Add(
                DriverCommand.TouchLongPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/longclick"));
            this.commandDictionary.Add(
                DriverCommand.TouchFlick,
                new CommandInfo("POST", "/session/{sessionId}/touch/flick"));
            this.commandDictionary.Add(DriverCommand.UploadFile, new CommandInfo("POST", "/session/{sessionId}/file"));
        }

        private void InitializeWiniumCommandDictionary()
        {
            this.commandDictionary.Add(
                DriverCommand.FindDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/cell/{row}/{column}"));

            this.commandDictionary.Add(
                DriverCommand.GetDataGridColumnCount,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/column/count"));

            this.commandDictionary.Add(
                DriverCommand.GetDataGridRowCount,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/row/count"));

            this.commandDictionary.Add(
                DriverCommand.ScrollToDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/scroll/{row}/{column}"));

            this.commandDictionary.Add(
                DriverCommand.SelectDataGridCell,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/select/{row}/{column}"));

            this.commandDictionary.Add(
                DriverCommand.IsComboBoxExpanded,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expanded"));

            this.commandDictionary.Add(
                DriverCommand.ExpandComboBox,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expand"));

            this.commandDictionary.Add(
                DriverCommand.CollapseComboBox,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/collapse"));

            this.commandDictionary.Add(
                DriverCommand.FindComboBoxSelectedItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/items/selected"));

            this.commandDictionary.Add(
                DriverCommand.ScrollToComboBoxItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/scroll"));

            this.commandDictionary.Add(
                DriverCommand.ScrollToListBoxItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/listbox/scroll"));

            this.commandDictionary.Add(
                DriverCommand.FindMenuItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/item/{path}"));

            this.commandDictionary.Add(
                DriverCommand.SelectMenuItem,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/select/{path}"));
        }

        #endregion
    }
}
