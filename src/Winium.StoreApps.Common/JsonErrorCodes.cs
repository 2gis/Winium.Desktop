namespace Winium.StoreApps.Common
{
    #region using

    using System.Collections.Generic;

    #endregion

    public static class JsonErrorCodes
    {
        // TODO: in the future ResponseStatus will be removed in favor of HTTPStatus (see https://w3c.github.io/webdriver/webdriver-spec.html#handling-errors)
        #region Static Fields

        private static readonly Dictionary<ResponseStatus, string> ErrorMap = new Dictionary<ResponseStatus, string>();

        #endregion

        #region Constructors and Destructors

        static JsonErrorCodes()
        {
            ErrorMap.Add(ResponseStatus.NoSuchElement, "no such element");
            ErrorMap.Add(ResponseStatus.NoSuchFrame, "no such frame");
            ErrorMap.Add(ResponseStatus.UnknownCommand, "unknown command");
            ErrorMap.Add(ResponseStatus.StaleElementReference, "stale element reference");
            ErrorMap.Add(ResponseStatus.ElementNotVisible, "element not visible");
            ErrorMap.Add(ResponseStatus.InvalidElementState, "invalid element state");
            ErrorMap.Add(ResponseStatus.UnknownError, "unknown error");
            ErrorMap.Add(ResponseStatus.ElementIsNotSelectable, "element not selectable");
            ErrorMap.Add(ResponseStatus.JavaScriptError, "javascript error");
            ErrorMap.Add(ResponseStatus.Timeout, "timeout");
            ErrorMap.Add(ResponseStatus.NoSuchWindow, "no such window");
            ErrorMap.Add(ResponseStatus.InvalidCookieDomain, "invalid cookie domain");
            ErrorMap.Add(ResponseStatus.UnableToSetCookie, "unable to set cookie");
            ErrorMap.Add(ResponseStatus.UnexpectedAlertOpen, "unexpected alert open");
            ErrorMap.Add(ResponseStatus.NoAlertOpenError, "no such alert");
            ErrorMap.Add(ResponseStatus.ScriptTimeout, "script timeout");
            ErrorMap.Add(ResponseStatus.InvalidElementCoordinates, "invalid element coordinates");
            ErrorMap.Add(ResponseStatus.InvalidSelector, "invalid selector");
            ErrorMap.Add(ResponseStatus.SessionNotCreatedException, "session not created");
            ErrorMap.Add(ResponseStatus.MoveTargetOutOfBounds, "move target out of bounds");

            // TODO: No match in ResponseStatus
            /*ErrorMap.Add(400, "invalid argument");
            ErrorMap.Add(404, "invalid session id"); 
            ErrorMap.Add(405, "unknown method"); 
            ErrorMap.Add(500, "unsupported operation");*/
        }

        #endregion

        #region Public Methods and Operators

        public static string Parse(ResponseStatus status)
        {
            return ErrorMap.ContainsKey(status) ? ErrorMap[status] : status.ToString();
        }

        #endregion
    }
}
