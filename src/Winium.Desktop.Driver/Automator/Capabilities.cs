namespace Winium.Desktop.Driver.Automator
{
    #region using

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Winium.Cruciatus.Settings;

    #endregion

    internal class Capabilities
    {
        #region Constructors and Destructors

        internal Capabilities()
        {
            this.App = string.Empty;
            this.Arguments = string.Empty;
            this.LaunchDelay = 0;
            this.DebugConnectToRunningApp = false;
            this.InnerPort = 9998;
            this.KeyboardSimulator = KeyboardSimulatorType.BasedOnInputSimulatorLib;
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
                            delegate(object sender, ErrorEventArgs args)
                                {
                                    args.ErrorContext.Handled = true;
                                }
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
