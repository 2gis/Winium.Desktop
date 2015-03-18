namespace Winium.StoreApps.Common.Exceptions
{
    #region

    using System;
    using System.Net;

    #endregion

    public class InnerDriverRequestException : Exception
    {
        #region Constructors and Destructors

        public InnerDriverRequestException()
        {
        }

        public InnerDriverRequestException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            this.StatusCode = statusCode;
        }

        public InnerDriverRequestException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public InnerDriverRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        #region Public Properties

        public HttpStatusCode StatusCode { get; set; }

        #endregion
    }
}
