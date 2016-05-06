namespace DotNetRemoteWebDriver
{
    public class CommandInfo
    {
        #region Constructors and Destructors

        public CommandInfo(string method, string resourcePath)
        {
            ResourcePath = resourcePath;
            Method = method;
        }

        #endregion

        #region Constants

        public const string DeleteCommand = "DELETE";

        public const string GetCommand = "GET";

        public const string PostCommand = "POST";

        #endregion

        #region Public Properties

        public string Method { get; set; }

        public string ResourcePath { get; set; }

        #endregion
    }
}