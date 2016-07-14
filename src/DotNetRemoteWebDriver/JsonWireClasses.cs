using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNetRemoteWebDriver
{
    public class JsonElementContent
    {
        public JsonElementContent(string element)
        {
            Element = element;
        }

        [JsonProperty("ELEMENT")]
        public string Element { get; set; }
    }

    public class JsonResponse
    {
        public JsonResponse(string sessionId, ResponseStatus responseCode, object value)
        {
            SessionId = sessionId;
            Status = responseCode;

            Value = responseCode == ResponseStatus.Success ? value : PrepareErrorResponse(value);
        }

        private object PrepareErrorResponse(object value)
        {
            var result = new Dictionary<string, string> {{"error", JsonErrorCodes.Parse(Status)}};

            string message;
            var exception = value as Exception;
            if (exception != null)
            {
                message = exception.Message;
                result.Add("stacktrace", exception.StackTrace);
            }
            else
            {
                message = value.ToString();
            }

            result.Add("message", message);
            return result;
        }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}