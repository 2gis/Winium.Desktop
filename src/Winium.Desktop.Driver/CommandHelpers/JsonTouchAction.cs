namespace Winium.Desktop.Driver.CommandHelpers
{
    #region usings

    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    class JsonTouchAction
    {
        #region Public Properties

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("options")]
        public Dictionary<string, string> Options { get; set; }

        #endregion
    }
}
