#region using

using System.Net;

#endregion

namespace DotNetRemoteWebDriver
{
    #region

    

    #endregion

    public class CommandResponse
    {
        #region Public Properties

        public string Content { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        #endregion

        #region Public Methods and Operators

        public static CommandResponse Create(HttpStatusCode code, string content)
        {
            return new CommandResponse {HttpStatusCode = code, Content = content};
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", HttpStatusCode, Content);
        }

        #endregion
    }
}