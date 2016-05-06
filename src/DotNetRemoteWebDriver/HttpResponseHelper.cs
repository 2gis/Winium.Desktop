#region using

using System.Collections.Generic;
using System.Net;
using System.Text;

#endregion

namespace DotNetRemoteWebDriver
{
    public static class HttpResponseHelper
    {
        static HttpResponseHelper()
        {
            StatusCodeDescriptors = new Dictionary<HttpStatusCode, string>
            {
                {HttpStatusCode.OK, "OK"},
                {HttpStatusCode.BadRequest, "Bad Request"},
                {HttpStatusCode.NotFound, "Not Found"},
                {HttpStatusCode.NotImplemented, "Not Implemented"}
            };
        }

        public static Dictionary<HttpStatusCode, string> StatusCodeDescriptors { get; }

        #region Constants

        private const string JsonContentType = "application/json;charset=UTF-8";

        private const string PlainTextContentType = "text/plain";

        #endregion

        #region Public Methods and Operators

        public static bool IsClientError(int code)
        {
            return code >= 400 && code < 500;
        }

        public static string ResponseString(HttpStatusCode statusCode, string content)
        {
            var contentType = IsClientError((int) statusCode) ? PlainTextContentType : JsonContentType;

            string statusDescription;
            StatusCodeDescriptors.TryGetValue(statusCode, out statusDescription);

            var responseString = new StringBuilder();
            responseString.AppendLine($"HTTP/1.1 {(int) statusCode} {statusDescription}");
            responseString.AppendLine($"Content-Type: {contentType}");
            responseString.AppendLine("Connection: close");
            responseString.AppendLine(string.Empty);
            responseString.AppendLine(content);

            return responseString.ToString();
        }

        #endregion
    }
}