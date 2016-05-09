using System;
using System.CodeDom;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace DotNetRemoteWebDriver
{
    public class Listener
    {
        public Listener(int listenerPort, IServiceProvider services)
        {
            Port = listenerPort;
            _services = services;
        }

        private UriDispatchTables _dispatcher;
        private CommandExecutorDispatchTable _executorDispatcher;

        private TcpListener _listener;
        private readonly IServiceProvider _services;
        private bool _cancelled;

        public static string UrnPrefix { get; set; }

        public int Port { get; }

        public Uri Prefix { get; private set; }

        public async void StartListening()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, Port);

                Prefix = new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}", Port));
                _dispatcher = new UriDispatchTables(new Uri(Prefix, UrnPrefix));
                _executorDispatcher = new CommandExecutorDispatchTable();

                _handler += Handler;
                SetConsoleCtrlHandler(_handler, true);

                _listener.Start();

                // Enter the listening loop
                Logger.Debug("Waiting for a connection...");
                while (!_cancelled)
                {
                    if (!_listener.Pending())
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    // Perform a blocking call to accept requests. 
                    var client = await _listener.AcceptTcpClientAsync();

                    // Get a stream object for reading and writing
                    using (var stream = client.GetStream())
                    {
                        var acceptedRequest = HttpRequest.ReadFromStreamWithoutClosing(stream);
                        Logger.Debug("ACCEPTED REQUEST {0}", acceptedRequest.StartingLine);

                        var response = HandleRequest(acceptedRequest);
                        using (var writer = new StreamWriter(stream))
                        {
                            try
                            {
                                writer.Write(response);
                                writer.Flush();
                            }
                            catch (IOException ex)
                            {
                                Logger.Error("Error occured while writing response: {0}", ex);
                            }
                        }

                        // Shutdown and end connection
                    }

                    client.Close();

                    Logger.Debug("Client closed\n");
                    Logger.Debug("Waiting for a connection...");
                }

                Automator.Automator.Clear();
            }
            catch (SocketException ex)
            {
                Logger.Error("SocketException occurred while trying to start listner: {0}", ex);
                throw;
            }
            catch (ArgumentException ex)
            {
                Logger.Error("ArgumentException occurred while trying to start listner: {0}", ex);
                throw;
            }
            finally
            {
                // Stop listening for new clients.
                _listener.Stop();
            }
        }

        public void StopListening()
        {
            _listener.Stop();
        }

        private string HandleRequest(HttpRequest acceptedRequest)
        {
            try
            {
                var firstHeaderTokens = acceptedRequest.StartingLine.Split(' ');
                var method = firstHeaderTokens[0];
                var resourcePath = firstHeaderTokens[1];

                var uriToMatch = new Uri(Prefix, resourcePath);
                var matched = _dispatcher.Match(method, uriToMatch);

                if (matched == null)
                {
                    Logger.Warn("Unknown command recived: {0}", uriToMatch);
                    return HttpResponseHelper.ResponseString(HttpStatusCode.NotFound, "Unknown command " + uriToMatch);
                }

                var commandName = matched.Data.ToString();
                var commandToExecute = new Command(commandName, acceptedRequest.MessageBody);
                foreach (string variableName in matched.BoundVariables.Keys)
                    commandToExecute.Parameters[variableName] = matched.BoundVariables[variableName];

                string sessionId;
                if (TryGetSession(matched.BoundVariables, out sessionId))
                    commandToExecute.SessionId = sessionId;


                var commandResponse = ProcessCommand(commandToExecute);
                return HttpResponseHelper.ResponseString(commandResponse.HttpStatusCode, commandResponse.Content);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to process request: " + e.Message, e);
                return HttpResponseHelper.ResponseString(HttpStatusCode.InternalServerError,
                    "Failed to process request: " + e.Message);
            }
        }

        private bool TryGetSession(NameValueCollection boundVariables, out string sessionId)
        {
            sessionId = boundVariables["SESSIONID"];
            return !string.IsNullOrEmpty(sessionId);
        }

        private CommandResponse ProcessCommand(Command command)
        {
            Logger.Info("COMMAND {0}\r\n{1}", command.Name, command.Parameters.ToString());
            var executor = _executorDispatcher.GetExecutor(command.Name);
            executor.Services = _services;
            executor.ExecutedCommand = command;
            var respnose = executor.Do();
            Logger.Debug("RESPONSE:\r\n{0}", respnose);

            return respnose;
        }

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(int sig);
        static EventHandler _handler;

        private bool Handler(int sig)
        {
            var ctrlTyp = new[] { "cancel", "break", "close", "logoff", "shutdown" };
            var eventName = ctrlTyp[sig];
            Logger.Info($"Recieved {eventName} signal. Shutting down.");
            _cancelled = true;
            _listener.Stop();
            return true;
        }

    }
}