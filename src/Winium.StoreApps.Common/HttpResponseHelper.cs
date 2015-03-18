namespace Winium.StoreApps.Common
{
    #region

    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    #endregion

    public static class HttpResponseHelper
    {
        #region Constants

        private const string JsonContentType = "application/json;charset=UTF-8";

        private const string PlainTextContentType = "text/plain";

        #endregion

        #region Static Fields

        private static Dictionary<HttpStatusCode, string> statusCodeDescriptors;

        #endregion

        #region Public Properties

        public static Dictionary<HttpStatusCode, string> StatusCodeDescriptors
        {
            get
            {
                return statusCodeDescriptors
                       ?? (statusCodeDescriptors =
                           new Dictionary<HttpStatusCode, string>
                               {
                                   { HttpStatusCode.OK, "OK" }, 
                                   { HttpStatusCode.BadRequest, "Bad Request" }, 
                                   { HttpStatusCode.NotFound, "Not Found" }, 
                                   { HttpStatusCode.NotImplemented, "Not Implemented" }
                               });
            }
        }

        #endregion

        #region Public Methods and Operators

        public static bool IsClientError(int code)
        {
            return code >= 400 && code < 500;
        }

        public static string ResponseString(HttpStatusCode statusCode, string content)
        {
            var contentType = IsClientError((int)statusCode) ? PlainTextContentType : JsonContentType;

            string statusDescription;
            StatusCodeDescriptors.TryGetValue(statusCode, out statusDescription);

            var responseString = new StringBuilder();
            responseString.AppendLine(string.Format("HTTP/1.1 {0} {1}", (int)statusCode, statusDescription));
            responseString.AppendLine(string.Format("Content-Type: {0}", contentType));
            responseString.AppendLine("Connection: close");
            responseString.AppendLine(string.Empty);
            responseString.AppendLine(content);

            return responseString.ToString();
        }

        #endregion
    }
}
