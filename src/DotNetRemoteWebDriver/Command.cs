#region using

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace DotNetRemoteWebDriver
{
    #region

    

    #endregion

    public class Command
    {
        #region Fields

        #endregion

        #region Constructors and Destructors

        public Command(string name, IDictionary<string, JToken> parameters)
        {
            Name = name;
            if (parameters != null) Parameters = parameters;
        }

        public Command(string name, string jsonParameters)
            : this(name, string.IsNullOrEmpty(jsonParameters) ? null : JObject.Parse(jsonParameters))
        {
        }

        public Command(string name)
        {
            Name = name;
        }

        public Command()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the command name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets the parameters of the command
        /// </summary>
        [JsonProperty("parameters")]
        public IDictionary<string, JToken> Parameters { get; private set; } = new JObject();

        /// <summary>
        ///     Gets the SessionID of the command
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        #endregion
    }
}