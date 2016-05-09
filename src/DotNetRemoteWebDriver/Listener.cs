#region using

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;

#endregion

namespace DotNetRemoteWebDriver
{
    #region using

    

    #endregion

    public class Listener
    {
        #region Static Fields

        private static string urnPrefix;

        #endregion

        #region Constructors and Destructors

        public Listener(int listenerPort, IServiceProvider services)
        {
            Port = listenerPort;
            _services = services;
        }

        #endregion

        #region Fields

        private UriDispatchTables _dispatcher;
        private CommandExecutorDispatchTable _executorDispatcher;

        private TcpListener _listener;
        private readonly IServiceProvider _services;

        #endregion

        #region Public Properties

        public static string UrnPrefix
        {
            get { return urnPrefix; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // Normalize prefix
                    urnPrefix = "/" + value.Trim('/');
                }
            }
        }

        public int Port { get; }

        public Uri Prefix { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void StartListening()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, Port);

                Prefix = new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}", Port));
                _dispatcher = new UriDispatchTables(new Uri(Prefix, UrnPrefix));
                _executorDispatcher = new CommandExecutorDispatchTable();

                // Start listening for client requests.
                _listener.Start();

                // Enter the listening loop
                while (true)
                {
                    Logger.Debug("Waiting for a connection...");

                    // Perform a blocking call to accept requests. 
                    var client = _listener.AcceptTcpClient();

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
                }
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

        #endregion

        #region Methods

        private string HandleRequest(HttpRequest acceptedRequest)
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

        #endregion
    }
}