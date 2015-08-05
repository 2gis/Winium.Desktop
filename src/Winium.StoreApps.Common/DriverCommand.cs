// Copied from OpenQA
namespace Winium.StoreApps.Common
{
    /// <summary>
    /// Values describing the list of commands understood by a remote server using the JSON wire protocol.
    /// 
    /// </summary>
    public static class DriverCommand
    {
        #region Static Fields

        #region Selenium

        /// <summary>
        /// Represents the AcceptAlert command
        /// 
        /// </summary>
        public static readonly string AcceptAlert = "acceptAlert";

        /// <summary>
        /// Represents adding a cookie command
        /// 
        /// </summary>
        public static readonly string AddCookie = "addCookie";

        /// <summary>
        /// Represents ClearElement command
        /// 
        /// </summary>
        public static readonly string ClearElement = "clearElement";

        /// <summary>
        /// Represents ClickElement command
        /// 
        /// </summary>
        public static readonly string ClickElement = "clickElement";

        /// <summary>
        /// Represents a Browser close command
        /// 
        /// </summary>
        public static readonly string Close = "close";

        /// <summary>
        /// Represents the Define Driver Mapping command
        /// 
        /// </summary>
        public static readonly string DefineDriverMapping = "defineDriverMapping";

        /// <summary>
        /// Represents Deleting all cookies command
        /// 
        /// </summary>
        public static readonly string DeleteAllCookies = "deleteAllCookies";

        /// <summary>
        /// Represents deleting a cookie command
        /// 
        /// </summary>
        public static readonly string DeleteCookie = "deleteCookie";

        /// <summary>
        /// Describes an element
        /// 
        /// </summary>
        public static readonly string DescribeElement = "describeElement";

        /// <summary>
        /// Represents the DismissAlert command
        /// 
        /// </summary>
        public static readonly string DismissAlert = "dismissAlert";

        /// <summary>
        /// Represents ElementEquals command
        /// 
        /// </summary>
        public static readonly string ElementEquals = "elementEquals";

        /// <summary>
        /// Represents ExecuteAsyncScript command
        /// 
        /// </summary>
        public static readonly string ExecuteAsyncScript = "executeAsyncScript";

        /// <summary>
        /// Represents ExecuteScript command
        /// 
        /// </summary>
        public static readonly string ExecuteScript = "executeScript";

        /// <summary>
        /// Represents FindChildElement command
        /// 
        /// </summary>
        public static readonly string FindChildElement = "findChildElement";

        /// <summary>
        /// Represents FindChildElements command
        /// 
        /// </summary>
        public static readonly string FindChildElements = "findChildElements";

        /// <summary>
        /// Represents FindElement command
        /// 
        /// </summary>
        public static readonly string FindElement = "findElement";

        /// <summary>
        /// Represents FindElements command
        /// 
        /// </summary>
        public static readonly string FindElements = "findElements";

        /// <summary>
        /// Represents a GET command
        /// 
        /// </summary>
        public static readonly string Get = "get";

        /// <summary>
        /// Represents GetActiveElement command
        /// 
        /// </summary>
        public static readonly string GetActiveElement = "getActiveElement";

        /// <summary>
        /// Represents the GetAlertText command
        /// 
        /// </summary>
        public static readonly string GetAlertText = "getAlertText";

        /// <summary>
        /// Represents getting all cookies command
        /// 
        /// </summary>
        public static readonly string GetAllCookies = "getCookies";

        /// <summary>
        /// Represents GetCurrentUrl command
        /// 
        /// </summary>
        public static readonly string GetCurrentUrl = "getCurrentUrl";

        /// <summary>
        /// Represents GetCurrentWindowHandle command
        /// 
        /// </summary>
        public static readonly string GetCurrentWindowHandle = "getCurrentWindowHandle";

        /// <summary>
        /// Represents GetElementAttribute command
        /// 
        /// </summary>
        public static readonly string GetElementAttribute = "getElementAttribute";

        /// <summary>
        /// Represents GetElementLocation command
        /// 
        /// </summary>
        public static readonly string GetElementLocation = "getElementLocation";

        /// <summary>
        /// Represents GetElementLocationOnceScrolledIntoView command
        /// 
        /// </summary>
        public static readonly string GetElementLocationOnceScrolledIntoView = "getElementLocationOnceScrolledIntoView";

        /// <summary>
        /// Represents GetElementSize command
        /// 
        /// </summary>
        public static readonly string GetElementSize = "getElementSize";

        /// <summary>
        /// Represents GetElementTagName command
        /// 
        /// </summary>
        public static readonly string GetElementTagName = "getElementTagName";

        /// <summary>
        /// Represents GetElementText command
        /// 
        /// </summary>
        public static readonly string GetElementText = "getElementText";

        /// <summary>
        /// Represents GetElementValueOfCSSProperty command
        /// 
        /// </summary>
        public static readonly string GetElementValueOfCssProperty = "getElementValueOfCssProperty";

        /// <summary>
        /// Represents GetOrientation command
        /// 
        /// </summary>
        public static readonly string GetOrientation = "getOrientation";

        /// <summary>
        /// Represents GetPageSource command
        /// 
        /// </summary>
        public static readonly string GetPageSource = "getPageSource";

        /// <summary>
        /// Represents the Get Session Capabilities command
        /// 
        /// </summary>
        public static readonly string GetSessionCapabilities = "getSessionCapabilities";

        /// <summary>
        /// Represents the Get Session List command
        /// 
        /// </summary>
        public static readonly string GetSessionList = "getSessionList";

        /// <summary>
        /// Represents GetTitle command
        /// 
        /// </summary>
        public static readonly string GetTitle = "getTitle";

        /// <summary>
        /// Represents GetWindowHandles command
        /// 
        /// </summary>
        public static readonly string GetWindowHandles = "getWindowHandles";

        /// <summary>
        /// Represents GetWindowPosition command
        /// 
        /// </summary>
        public static readonly string GetWindowPosition = "getWindowPosition";

        /// <summary>
        /// Represents GetWindowSize command
        /// 
        /// </summary>
        public static readonly string GetWindowSize = "getWindowSize";

        /// <summary>
        /// Represents a Browser going back command
        /// 
        /// </summary>
        public static readonly string GoBack = "goBack";

        /// <summary>
        /// Represents a Browser going forward command
        /// 
        /// </summary>
        public static readonly string GoForward = "goForward";

        /// <summary>
        /// Represents the ImplicitlyWait command
        /// 
        /// </summary>
        public static readonly string ImplicitlyWait = "implicitlyWait";

        /// <summary>
        /// Represents IsElementDisplayed command
        /// 
        /// </summary>
        public static readonly string IsElementDisplayed = "isElementDisplayed";

        /// <summary>
        /// Represents IsElementEnabled command
        /// 
        /// </summary>
        public static readonly string IsElementEnabled = "isElementEnabled";

        /// <summary>
        /// Represents IsElementSelected command
        /// 
        /// </summary>
        public static readonly string IsElementSelected = "isElementSelected";

        /// <summary>
        /// Represents MaximizeWindow command
        /// 
        /// </summary>
        public static readonly string MaximizeWindow = "maximizeWindow";

        /// <summary>
        /// Represents the MouseClick command.
        /// 
        /// </summary>
        public static readonly string MouseClick = "mouseClick";

        /// <summary>
        /// Represents the MouseDoubleClick command.
        /// 
        /// </summary>
        public static readonly string MouseDoubleClick = "mouseDoubleClick";

        /// <summary>
        /// Represents the MouseDown command.
        /// 
        /// </summary>
        public static readonly string MouseDown = "mouseDown";

        /// <summary>
        /// Represents the MouseMoveTo command.
        /// 
        /// </summary>
        public static readonly string MouseMoveTo = "mouseMoveTo";

        /// <summary>
        /// Represents the MouseUp command.
        /// 
        /// </summary>
        public static readonly string MouseUp = "mouseUp";

        /// <summary>
        /// Represents a New Session command
        /// 
        /// </summary>
        public static readonly string NewSession = "newSession";

        /// <summary>
        /// Represents a browser quit command
        /// 
        /// </summary>
        public static readonly string Quit = "quit";

        /// <summary>
        /// Represents a Browser refreshing command
        /// 
        /// </summary>
        public static readonly string Refresh = "refresh";

        /// <summary>
        /// Represents Screenshot command
        /// 
        /// </summary>
        public static readonly string Screenshot = "screenshot";

        /// <summary>
        /// Represents the SendKeysToActiveElement command.
        /// 
        /// </summary>
        public static readonly string SendKeysToActiveElement = "sendKeysToActiveElement";

        /// <summary>
        /// Represents SendKeysToElements command
        /// 
        /// </summary>
        public static readonly string SendKeysToElement = "sendKeysToElement";

        /// <summary>
        /// Represents the SetAlertValue command
        /// 
        /// </summary>
        public static readonly string SetAlertValue = "setAlertValue";

        /// <summary>
        /// Represents the SetAsyncScriptTimeout command
        /// 
        /// </summary>
        public static readonly string SetAsyncScriptTimeout = "setScriptTimeout";

        /// <summary>
        /// Represents SetOrientation command
        /// 
        /// </summary>
        public static readonly string SetOrientation = "setOrientation";

        /// <summary>
        /// Represents the SetTimeout command
        /// 
        /// </summary>
        public static readonly string SetTimeout = "setTimeout";

        /// <summary>
        /// Represents SetWindowPosition command
        /// 
        /// </summary>
        public static readonly string SetWindowPosition = "setWindowPosition";

        /// <summary>
        /// Represents SetWindowSize command
        /// 
        /// </summary>
        public static readonly string SetWindowSize = "setWindowSize";

        /// <summary>
        /// Represents the Status command.
        /// 
        /// </summary>
        public static readonly string Status = "status";

        /// <summary>
        /// Represents SubmitElement command
        /// 
        /// </summary>
        public static readonly string SubmitElement = "submitElement";

        /// <summary>
        /// Represents SwitchToFrame command
        /// 
        /// </summary>
        public static readonly string SwitchToFrame = "switchToFrame";

        /// <summary>
        /// Represents SwitchToParentFrame command
        /// 
        /// </summary>
        public static readonly string SwitchToParentFrame = "switchToParentFrame";

        /// <summary>
        /// Represents SwitchToWindow command
        /// 
        /// </summary>
        public static readonly string SwitchToWindow = "switchToWindow";

        /// <summary>
        /// Represents the TouchDoubleTap command.
        /// 
        /// </summary>
        public static readonly string TouchDoubleTap = "touchDoubleTap";

        /// <summary>
        /// Represents the TouchFlick command.
        /// 
        /// </summary>
        public static readonly string TouchFlick = "touchFlick";

        /// <summary>
        /// Represents the TouchLongPress command.
        /// 
        /// </summary>
        public static readonly string TouchLongPress = "touchLongPress";

        /// <summary>
        /// Represents the TouchMove command.
        /// 
        /// </summary>
        public static readonly string TouchMove = "touchMove";

        /// <summary>
        /// Represents the TouchPress command.
        /// 
        /// </summary>
        public static readonly string TouchPress = "touchDown";

        /// <summary>
        /// Represents the TouchRelease command.
        /// 
        /// </summary>
        public static readonly string TouchRelease = "touchUp";

        /// <summary>
        /// Represents the TouchScroll command.
        /// 
        /// </summary>
        public static readonly string TouchScroll = "touchScroll";

        /// <summary>
        /// Represents the TouchSingleTap command.
        /// 
        /// </summary>
        public static readonly string TouchSingleTap = "touchSingleTap";

        /// <summary>
        /// Represents the UploadFile command.
        /// 
        /// </summary>
        public static readonly string UploadFile = "uploadFile";

        #endregion

        #region Winium

        /// <summary>
        /// Represents additional driver commnad FindDataGridCell.
        /// 
        /// </summary>
        public static readonly string FindDataGridCell = "findDataGridCell";

        /// <summary>
        /// Represents additional driver commnad GetDataGridColumnCount.
        /// 
        /// </summary>
        public static readonly string GetDataGridColumnCount = "getDataGridColumnCount";

        /// <summary>
        /// Represents additional driver commnad GetDataGridRowCount.
        /// 
        /// </summary>
        public static readonly string GetDataGridRowCount = "getDataGridRowCount";

        /// <summary>
        /// Represents additional driver commnad ScrollToDataGridCell.
        /// 
        /// </summary>
        public static readonly string ScrollToDataGridCell = "scrollToDataGridCell";

        /// <summary>
        /// Represents additional driver commnad ScrollToDataGridCell.
        /// 
        /// </summary>
        public static readonly string SelectDataGridCell = "selectDataGridCell";

        /// <summary>
        /// Represents additional driver commnad IsComboBoxExpanded.
        /// 
        /// </summary>
        public static readonly string IsComboBoxExpanded = "isComboBoxExpanded";

        /// <summary>
        /// Represents additional driver commnad ExpandComboBox.
        /// 
        /// </summary>
        public static readonly string ExpandComboBox = "expandComboBox";

        /// <summary>
        /// Represents additional driver commnad CollapseComboBox.
        /// 
        /// </summary>
        public static readonly string CollapseComboBox = "collapseComboBox";

        /// <summary>
        /// Represents additional driver commnad FindComboBoxSelectedItem.
        /// 
        /// </summary>
        public static readonly string FindComboBoxSelectedItem = "findComboBoxSelectedItem";

        /// <summary>
        /// Represents additional driver commnad ScrollToComboBoxItem.
        /// 
        /// </summary>
        public static readonly string ScrollToComboBoxItem = "scrollToComboBoxItem";

        /// <summary>
        /// Represents additional driver commnad ScrollToListBoxItem.
        /// 
        /// </summary>
        public static readonly string ScrollToListBoxItem = "scrollToListBoxItem";

        /// <summary>
        /// Represents additional driver commnad FindMenuItem.
        /// 
        /// </summary>
        public static readonly string FindMenuItem = "findMenuItem";

        /// <summary>
        /// Represents additional driver commnad SelectMenuItem.
        /// 
        /// </summary>
        public static readonly string SelectMenuItem = "selectMenuItem";

        #endregion

        #endregion
    }
}
