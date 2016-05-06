namespace DotNetRemoteWebDriver.Exceptions
{
    #region

    using System;

    #endregion

    public class AutomationException : Exception
    {
        #region Fields

        #endregion

        #region Constructors and Destructors

        public AutomationException()
        {
        }

        public AutomationException(string message, ResponseStatus status)
            : base(message)
        {
            this.Status = status;
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

        #region Public Properties

        public ResponseStatus Status { get; set; } = ResponseStatus.UnknownError;

        #endregion
    }
}
