using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
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
                Running = true;

                // Enter the listening loop
                Logger.Log.Debug("Waiting for a connection...");
                while (!_cancelled && Running)
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
                        Logger.Log.Debug($"ACCEPTED REQUEST {acceptedRequest.StartingLine}");

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
                                Logger.Log.Error("Error occured while writing response: {0}", ex);
                            }
                        }

                        // Shutdown and end connection
                    }

                    client.Close();

                    Logger.Log.Debug("Client closed\n");
                    Logger.Log.Debug("Waiting for a connection...");
                }

                Automator.Automator.Clear();
            }
            catch (SocketException ex)
            {
                Logger.Log.Error("SocketException occurred while trying to start listner: {0}", ex);
                throw;
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error("ArgumentException occurred while trying to start listner: {0}", ex);
                throw;
            }
            catch (Exception e)
            {
                Logger.Log.Error($"Unexpected exception occurred while trying to start listener: {e}");
            }
            finally
            {
                // Stop listening for new clients.
                _listener.Stop();
                Running = false;
            }
        }

        public bool Running { get; private set; }

        public void StopListening()
        {
            _listener.Stop();
            Running = false;
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
                    Logger.Log.Warn($"Unknown command recived: {uriToMatch}");
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
                Logger.Log.Error("Failed to process request: " + e.Message, e);
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
            Logger.Log.Info($"COMMAND {command.Name}\r\n{command.Parameters.ToString()}");
            var executor = _executorDispatcher.GetExecutor(command.Name);
            executor.Services = _services;
            executor.ExecutedCommand = command;
            var response = executor.Do();
            Logger.Log.Debug($"RESPONSE:\r\n{response}");

            return response;
        }

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(int sig);
        static EventHandler _handler;

        private bool Handler(int sig)
        {
            var ctrlTyp = new[] { "cancel", "break", "close", "logoff", "shutdown" };
            var eventName = ctrlTyp[sig];
            Logger.Log.Info($"Recieved {eventName} signal. Shutting down.");
            _cancelled = true;
            _listener.Stop();
            return true;
        }

    }
}