namespace Winium.StoreApps.Common
{
    #region using

    using System.Collections.Generic;

    #endregion

    public static class JsonErrorCodes
    {
        // TODO: in future, ResponseStatus will be changed to HTTPStatus (by w3c)
        #region Static Fields

        private static readonly Dictionary<ResponseStatus, string> Dict = new Dictionary<ResponseStatus, string>();

        #endregion

        #region Constructors and Destructors

        static JsonErrorCodes()
        {
            Dict.Add(ResponseStatus.NoSuchElement, "no such element");
            Dict.Add(ResponseStatus.NoSuchFrame, "no such frame");
            Dict.Add(ResponseStatus.UnknownCommand, "unknown command");
            Dict.Add(ResponseStatus.StaleElementReference, "stale element reference");
            Dict.Add(ResponseStatus.ElementNotVisible, "element not visible");
            Dict.Add(ResponseStatus.InvalidElementState, "invalid element state");
            Dict.Add(ResponseStatus.UnknownError, "unknown error");
            Dict.Add(ResponseStatus.ElementIsNotSelectable, "element not selectable");
            Dict.Add(ResponseStatus.JavaScriptError, "javascript error");
            Dict.Add(ResponseStatus.Timeout, "timeout");
            Dict.Add(ResponseStatus.NoSuchWindow, "no such window");
            Dict.Add(ResponseStatus.InvalidCookieDomain, "invalid cookie domain");
            Dict.Add(ResponseStatus.UnableToSetCookie, "unable to set cookie");
            Dict.Add(ResponseStatus.UnexpectedAlertOpen, "unexpected alert open");
            Dict.Add(ResponseStatus.NoAlertOpenError, "no such alert");
            Dict.Add(ResponseStatus.ScriptTimeout, "script timeout");
            Dict.Add(ResponseStatus.InvalidElementCoordinates, "invalid element coordinates");
            Dict.Add(ResponseStatus.InvalidSelector, "invalid selector");
            Dict.Add(ResponseStatus.SessionNotCreatedException, "session not created");
            Dict.Add(ResponseStatus.MoveTargetOutOfBounds, "move target out of bounds");

            // TODO: No match in ResponseStatus
            /*Dict.Add(400, "invalid argument");
            Dict.Add(404, "invalid session id"); 
            Dict.Add(405, "unknown method"); 
            Dict.Add(500, "unsupported operation");*/
        }

        #endregion

        #region Public Methods and Operators

        public static string Parse(ResponseStatus status)
        {
            return Dict.ContainsKey(status) ? Dict[status] : status.ToString();
        }

        #endregion
    }
}
