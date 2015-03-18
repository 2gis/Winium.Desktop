namespace Winium.StoreApps.Common
{
    #region

    using System.Net;

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
            return new CommandResponse { HttpStatusCode = code, Content = content };
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.HttpStatusCode, this.Content);
        }

        #endregion
    }
}
