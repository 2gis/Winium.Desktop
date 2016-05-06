#region using

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Winium.Cruciatus.Settings;

#endregion

namespace DotNetRemoteWebDriver.Automator
{
    #region using

    

    #endregion

    internal class Capabilities
    {
        #region Constructors and Destructors

        internal Capabilities()
        {
            App = string.Empty;
            Arguments = string.Empty;
            LaunchDelay = 0;
            DebugConnectToRunningApp = false;
            InnerPort = 9998;
            KeyboardSimulator = KeyboardSimulatorType.BasedOnInputSimulatorLib;
        }

        #endregion

        #region Public Properties

        [JsonProperty("app")]
        public string App { get; set; }

        [JsonProperty("args")]
        public string Arguments { get; set; }

        [JsonProperty("debugConnectToRunningApp")]
        public bool DebugConnectToRunningApp { get; set; }

        [JsonProperty("innerPort")]
        public int InnerPort { get; set; }

        [JsonProperty("keyboardSimulator")]
        public KeyboardSimulatorType KeyboardSimulator { get; set; }

        [JsonProperty("launchDelay")]
        public int LaunchDelay { get; set; }

        #endregion

        #region Public Methods and Operators

        public static Capabilities CapabilitiesFromJsonString(string jsonString)
        {
            var capabilities = JsonConvert.DeserializeObject<Capabilities>(
                jsonString,
                new JsonSerializerSettings
                {
                    Error =
                        delegate(object sender, ErrorEventArgs args) { args.ErrorContext.Handled = true; }
                });

            return capabilities;
        }

        public string CapabilitiesToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}