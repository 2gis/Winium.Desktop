#region using

using System.Reflection;
using Newtonsoft.Json;

#endregion

namespace DotNetRemoteWebDriver.CommandHelpers
{
    #region using

    

    #endregion

    public class BuildInfo
    {
        #region Static Fields

        private static string version;

        #endregion

        #region Public Properties

        [JsonProperty("version")]
        public string Version
        {
            get { return version ?? (version = Assembly.GetExecutingAssembly().GetName().Version.ToString()); }
        }

        #endregion
    }
}