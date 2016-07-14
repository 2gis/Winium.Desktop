#region using

using System;

#endregion

namespace DotNetRemoteWebDriver.Exceptions
{
    #region

    

    #endregion

    public class AutomationException : Exception
    {
        #region Public Properties

        public ResponseStatus Status { get; set; } = ResponseStatus.UnknownError;

        #endregion

        #region Fields

        #endregion

        #region Constructors and Destructors

        public AutomationException()
        {
        }

        public AutomationException(string message, ResponseStatus status)
            : base(message)
        {
            Status = status;
        }

        public AutomationException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public AutomationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}