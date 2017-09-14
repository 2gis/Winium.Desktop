namespace Winium.Desktop.Driver
{
    #region

    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Timers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Timer = System.Timers.Timer;

    #endregion

    public class NodeRegistrar
    {
        #region Fields

        private readonly string configFilePath;

        private readonly string defaultHost;

        private readonly int defaultPort;

        private Timer autoRegisterTimer;

        private string data;

        private NodeRegistrarConfiguration registrarConfiguration;

        #endregion

        #region Constructors and Destructors

        public NodeRegistrar(string configFilePath, string defaultHost, int defaultPort)
        {
            this.configFilePath = configFilePath;
            this.defaultHost = defaultHost;
            this.defaultPort = defaultPort;
        }

        #endregion

        #region Public Methods and Operators

        public void Register()
        {
            new Thread(
                () =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        this.RegisterNode();
                    }).Start();
        }

        #endregion

        #region Methods

        private void AutoRegisterEvent(object source, ElapsedEventArgs e)
        {
            if (!this.IsAlreadyRegistered())
            {
                this.PostData();
            }
        }

        private bool IsAlreadyRegistered()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var uri = new Uri(this.registrarConfiguration.HubUri, "/grid/api/proxy");
                    client.QueryString.Add("id", this.registrarConfiguration.Id);
                    dynamic jo = JObject.Parse(client.DownloadString(uri));
                    return jo.success;
                }
            }
            catch (WebException webException)
            {
                Logger.Error("Selenium Grid hub down or not responding: {0}.", webException.Message);
            }

            return false;
        }

        private bool LoadConfiguration()
        {
            try
            {
                dynamic jo = JObject.Parse(File.ReadAllText(this.configFilePath));

                if (jo.configuration.host == null || jo.configuration.port == null || jo.configuration.url == null)
                {
                    jo.configuration.host = this.defaultHost;
                    jo.configuration.port = this.defaultPort;
                    jo.configuration.url = new UriBuilder("http", this.defaultHost, this.defaultPort).ToString();
                    Logger.Warn(
                        "Some of required node options (host, port or url) are not set, Winium set them to: host={0}, port={1}, url={2}."
                        + " Note that this will not work if your node and grid aren't in the same place.", 
                        jo.configuration.host, 
                        jo.configuration.port, 
                        jo.configuration.url);
                }

                this.registrarConfiguration = jo.configuration.ToObject<NodeRegistrarConfiguration>();

                this.data = JsonConvert.SerializeObject(jo, Formatting.Indented);

                return true;
            }
            catch (JsonReaderException ex)
            {
                Logger.Error("Syntax error in node configuration file: {0}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Error reading node configuration: {0}", ex.Message);
                return false;
            }
        }

        private void PostData()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var uri = new Uri(this.registrarConfiguration.HubUri, "grid/register/");
                    var response = client.UploadString(uri, this.data);

                    Logger.Debug(
                        "Winium successfully registered with the grid on {0}, grid responded with: '{1}'.", 
                        this.registrarConfiguration.HubUri, 
                        response);
                }
            }
            catch (WebException webException)
            {
                var webResponse = webException.Response as HttpWebResponse;
                if (webResponse != null)
                {
                    Logger.Error("Selenium Grid refused to register hub. {0}", webResponse.StatusDescription);
                }
                else
                {
                    Logger.Error("Selenium Grid hub down or not responding: {0}.", webException.Message);
                }
            }
        }

        private void RegisterNode()
        {
            if (!this.LoadConfiguration())
            {
                return;
            }

            if (!this.registrarConfiguration.Register)
            {
                Logger.Debug("Node registration data was not send to Selenium Grid.");
                return;
            }

            this.PostData();

            if (!(this.registrarConfiguration.RegisterCycle > 0))
            {
                return;
            }

            Logger.Debug(
                "Starting auto register for grid. Will try to register every {0} ms", 
                this.registrarConfiguration.RegisterCycle);
            this.autoRegisterTimer = new Timer();
            this.autoRegisterTimer.Elapsed += this.AutoRegisterEvent;
            this.autoRegisterTimer.Interval = this.registrarConfiguration.RegisterCycle;
            this.autoRegisterTimer.Enabled = true;
        }

        #endregion

        private class NodeRegistrarConfiguration
        {
            #region Fields

            private Uri hubUri;

            private string id;

            #endregion

            #region Public Properties

            public Uri HubUri
            {
                get
                {
                    return this.hubUri ?? (this.hubUri = new UriBuilder("http", this.HubHost, this.HubPort).Uri);
                }
            }

            public string Id
            {
                get
                {
                    return this.id ?? (this.id = new UriBuilder("http", this.Host, this.Port).ToString());
                }
            }

            [JsonProperty("register")]
            public bool Register { get; set; }

            [JsonProperty("registerCycle")]
            public double RegisterCycle { get; set; }

            #endregion

            #region Properties

            [JsonProperty("host")]
            private string Host { get; set; }

            [JsonProperty("hubHost")]
            private string HubHost { get; set; }

            [JsonProperty("hubPort")]
            private int HubPort { get; set; }

            [JsonProperty("port")]
            private int Port { get; set; }

            #endregion
        }
    }
}
