namespace Winium.Desktop.Driver.CommandHelpers
{
    #region using

    using System;

    using Newtonsoft.Json;

    #endregion

    // ReSharper disable once InconsistentNaming
    public class OSInfo
    {
        #region Static Fields

        private static string architecture;

        private static string version;

        #endregion

        #region Public Properties

        [JsonProperty("arch")]
        public string Architecture
        {
            get
            {
                return architecture ?? (architecture = Environment.Is64BitOperatingSystem ? "x64" : "x86");
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return "windows";
            }
        }

        [JsonProperty("version")]
        public string Version
        {
            get
            {
                return version ?? (version = Environment.OSVersion.VersionString);
            }
        }

        #endregion
    }
}
