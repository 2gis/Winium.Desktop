﻿using System;

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
            new PriorCleanup { Port = _port }.Run();

            var services = new ServiceProvider();
            _processMonitor = new DriverProcessMonitor();
            services.Register<IDriverProcessMonitor>(_processMonitor);
            _listener = new Listener(_port, services);
            Listener.UrnPrefix = _urlBase;
            Console.WriteLine("Starting remote web driver on port {0}\n", _port);

            _listener.StartListening();
        }
    }
}