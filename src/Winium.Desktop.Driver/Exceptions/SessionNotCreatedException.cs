namespace Winium.Desktop.Driver.Exceptions
{
    #region

    using System;

    #endregion

    public class SessionNotCreatedException : Exception
    {

        #region Constructors and Destructors

        public SessionNotCreatedException()
        {
        }

        public SessionNotCreatedException(string message)
            : base(message)
        {
        }

        public SessionNotCreatedException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public SessionNotCreatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
