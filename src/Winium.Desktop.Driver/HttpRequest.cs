namespace Winium.Desktop.Driver
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    #endregion

    public class HttpRequest
    {
        #region Public Properties

        public Dictionary<string, string> Headers { get; set; }

        public string MessageBody { get; private set; }

        public string StartingLine { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static HttpRequest ReadFromStreamWithoutClosing(Stream stream)
        {
            var request = new HttpRequest();
            var streamReader = new StreamReader(stream);

            request.StartingLine = streamReader.ReadLine();

            request.Headers = ReadHeaders(streamReader);

            var contentLength = GetContentLength(request.Headers);
            request.MessageBody = contentLength != 0 ? ReadContent(streamReader, contentLength) : string.Empty;

            return request;
        }

        #endregion

        #region Methods

        private static int GetContentLength(IReadOnlyDictionary<string, string> headers)
        {
            var contentLength = 0;
            string contentLengthString;
            if (headers.TryGetValue("Content-Length", out contentLengthString))
            {
                contentLength = Convert.ToInt32(contentLengthString, CultureInfo.InvariantCulture);
            }

            return contentLength;
        }

        // reads the content of a request depending on its length
        private static string ReadContent(TextReader textReader, int contentLength)
        {
            var readBuffer = new char[contentLength];
            textReader.Read(readBuffer, 0, readBuffer.Length);
            return readBuffer.Aggregate(string.Empty, (current, ch) => current + ch);
        }

        private static Dictionary<string, string> ReadHeaders(TextReader textReader)
        {
            var headers = new Dictionary<string, string>();
            string header;
            while (!string.IsNullOrEmpty(header = textReader.ReadLine()))
            {
                var splitHeader = header.Split(':');
                headers.Add(splitHeader[0], splitHeader[1].Trim(' '));
            }

            return headers;
        }

        #endregion
    }
}
