namespace Winium.StoreApps.Common
{
    public class CommandInfo
    {
        #region Constants

        public const string DeleteCommand = "DELETE";

        public const string GetCommand = "GET";

        public const string PostCommand = "POST";

        #endregion

        #region Constructors and Destructors

        public CommandInfo(string method, string resourcePath)
        {
            this.ResourcePath = resourcePath;
            this.Method = method;
        }

        #endregion

        #region Public Properties

        public string Method { get; set; }

        public string ResourcePath { get; set; }

        #endregion
    }
}
