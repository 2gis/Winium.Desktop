namespace Winium.Desktop.Driver
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class Requester
    {
        #region Fields

        private readonly string ip;

        private readonly int port;

        #endregion

        #region Constructors and Destructors

        public Requester(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        #endregion

        #region Public Methods and Operators

        public string ForwardCommand(Command commandToForward, bool verbose = true, int timeout = 0)
        {
            var serializedCommand = JsonConvert.SerializeObject(commandToForward);

            var response = this.SendRequest(serializedCommand, verbose, timeout);
            if (response.Key == HttpStatusCode.OK)
            {
                return response.Value;
            }

            throw new InnerDriverRequestException(response.Value, response.Key);
        }

        public KeyValuePair<HttpStatusCode, string> SendRequest(string requestContent, bool verbose, int timeout)
        {
            var result = string.Empty;
            StreamReader reader = null;
            HttpWebResponse response = null;
            var status = HttpStatusCode.OK;
            try
            {
                // create the request
                var uri = string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}", this.ip, this.port);
                var request = CreateWebRequest(uri, requestContent);
                if (timeout != 0)
                {
                    request.Timeout = timeout;
                }

                if (verbose)
                {
                    Logger.Debug("Sending request to inner driver: {0}", uri);
                }

                // send the request and get the response
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                }
                catch (WebException ex)
                {
                    response = ex.Response as HttpWebResponse;
                }

                if (response != null)
                {
                    status = response.StatusCode;
                    var stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        throw new NullReferenceException("No response stream.");
                    }

                    // read and return the response
                    reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                if (verbose)
                {
                    // No need to log exceptions raised when sending service commands like ping.
                    Logger.Error("Error occurred while trying to send request to inner driver: {0}", ex);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }

            return new KeyValuePair<HttpStatusCode, string>(status, result);
        }

        #endregion

        #region Methods

        private static HttpWebRequest CreateWebRequest(string uri, string content)
        {
            // create request
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.KeepAlive = false;

            // write request body
            if (!string.IsNullOrEmpty(content))
            {
                var writer = new StreamWriter(request.GetRequestStream());
                writer.Write(content);
                writer.Close();
            }

            return request;
        }

        #endregion
    }
}
