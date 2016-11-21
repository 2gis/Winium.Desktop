using System;

namespace DotNetRemoteWebDriver
{
    internal class DriverHost : IDisposable
    {
        private readonly int _port;
        private readonly string _urlBase;
        private Listener _listener;
        private DriverProcessMonitor _processMonitor;

        public DriverHost(int port, string urlBase)
        {
            _port = port;
            _urlBase = urlBase;

            if (!string.IsNullOrEmpty(_urlBase))
                _urlBase = "/" + _urlBase.Trim('/');
        }

        public bool Running => _listener != null && _listener.Running;

        public void Dispose()
        {
            _listener.StopListening();
            _processMonitor.Dispose();
        }

        public void Run()
        {
            Logger.Log.Debug("Start of DriverHost.Run()");
            new PriorCleanup(_port).Run();

            Logger.Log.Debug("Ready to start new remote WebDriver.");
            var services = new ServiceProvider();
            _processMonitor = new DriverProcessMonitor();
            services.Register<IDriverProcessMonitor>(_processMonitor);
            _listener = new Listener(_port, services);
            Listener.UrnPrefix = _urlBase;

            Logger.Log.Info($"Starting remote web driver on port {_port}\n");
            _listener.StartListening();
        }
    }
}