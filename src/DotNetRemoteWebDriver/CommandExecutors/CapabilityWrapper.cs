using Newtonsoft.Json;
using OpenQA.Selenium;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class CapabilityWrapper
    {
        public CapabilityWrapper(ICapabilities capabilities)
        {
            BrowserName = capabilities.BrowserName;
            IsJavaScriptEnabled = capabilities.IsJavaScriptEnabled;
            Platform = capabilities.Platform.PlatformType.ToString();
            Version = capabilities.Version;
        }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonProperty(PropertyName = "isJavaScriptEnabled")]
        public bool IsJavaScriptEnabled { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "browserName")]
        public string BrowserName { get; set; }
    }
}