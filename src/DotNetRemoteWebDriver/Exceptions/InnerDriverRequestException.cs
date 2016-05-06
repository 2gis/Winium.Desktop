#region using

using System;
using System.Net;

#endregion

namespace DotNetRemoteWebDriver.Exceptions
{
    #region

    

    #endregion

    public class InnerDriverRequestException : Exception
    {
        #region Public Properties

        public HttpStatusCode StatusCode { get; set; }

        #endregion

        #region Constructors and Destructors

        public InnerDriverRequestException()
        {
        }

        public InnerDriverRequestException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
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
    }
}