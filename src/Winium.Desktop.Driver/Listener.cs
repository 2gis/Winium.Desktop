namespace Winium.Desktop.Driver
{
    #region using

    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    using Winium.StoreApps.Common;

    #endregion

    public class Listener
    {
        #region Static Fields

        private static string urnPrefix;

        #endregion

        #region Fields

        private UriDispatchTables dispatcher;

        private CommandExecutorDispatchTable executorDispatcher;

        private TcpListener listener;

        #endregion

        #region Constructors and Destructors

        public Listener(int listenerPort)
        {
            this.Port = listenerPort;
        }

        #endregion

        #region Public Properties

        public static string UrnPrefix
        {
            get
            {
                return urnPrefix;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // Normalize prefix
                    urnPrefix = "/" + value.Trim('/');
                }
            }
        }

        public int Port { get; private set; }

        public Uri Prefix { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void StartListening()
        {
            try
            {
                this.listener = new TcpListener(IPAddress.Any, this.Port);

                this.Prefix = new Uri(string.Format(CultureInfo.InvariantCulture, $"http://localhost:{this.Port}"));
                this.dispatcher = new UriDispatchTables(new Uri(this.Prefix, UrnPrefix));
                this.executorDispatcher = new CommandExecutorDispatchTable();

                // Start listening for client requests.
                this.listener.Start();
                Console.WriteLine($"Successfully started on port {this.Port}.");

                // Enter the listening loop
                while (true)
                {
                    Logger.Debug("Waiting for a connection...");

                    // Perform a blocking call to accept requests. 
                    var client = this.listener.AcceptTcpClient();

                    // Get a stream object for reading and writing
                    using (var stream = client.GetStream())
                    {
                        var acceptedRequest = HttpRequest.ReadFromStreamWithoutClosing(stream);

                        if (string.IsNullOrWhiteSpace(acceptedRequest.StartingLine))
                        {
                            Logger.Warn("ACCEPTED EMPTY REQUEST");
                        }
                        else
                        {
                            Logger.Debug($"ACCEPTED REQUEST {acceptedRequest.StartingLine}");

                            var response = this.HandleRequest(acceptedRequest);
                            using (var writer = new StreamWriter(stream))
                            {
                                try
                                {
                                    writer.Write(response);
                                    writer.Flush();
                                }
                                catch (IOException ex)
                                {
                                    Logger.Error($"Error occured while writing response: {ex}");
                                }
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
                Logger.Error($"SocketException occurred while trying to start listner: {ex}");
                throw;
            }
            catch (ArgumentException ex)
            {
                Logger.Error($"ArgumentException occurred while trying to start listner: {ex}");
                throw;
            }
            finally
            {
                // Stop listening for new clients.
                this.listener.Stop();
            }
        }

        public void StopListening()
        {
            this.listener.Stop();
        }

        #endregion

        #region Methods

        private string HandleRequest(HttpRequest acceptedRequest)
        {
            var firstHeaderTokens = acceptedRequest.StartingLine.Split(' ');
            var method = firstHeaderTokens[0];
            var resourcePath = firstHeaderTokens[1];

            var uriToMatch = new Uri(this.Prefix, resourcePath);
            var matched = this.dispatcher.Match(method, uriToMatch);

            if (matched == null)
            {
                Logger.Warn($"Unknown command recived: {uriToMatch}");
                return HttpResponseHelper.ResponseString(HttpStatusCode.NotFound, "Unknown command " + uriToMatch);
            }

            var commandName = matched.Data.ToString();
            var commandToExecute = new Command(commandName, acceptedRequest.MessageBody);
            foreach (string variableName in matched.BoundVariables.Keys)
            {
                commandToExecute.Parameters[variableName] = matched.BoundVariables[variableName];
            }

            var commandResponse = this.ProcessCommand(commandToExecute);
            return HttpResponseHelper.ResponseString(commandResponse.HttpStatusCode, commandResponse.Content);
        }

        private CommandResponse ProcessCommand(Command command)
        {
            Logger.Info($"COMMAND {command.Name}\r\n{command.Parameters.ToString()}");
            var executor = this.executorDispatcher.GetExecutor(command.Name);
            executor.ExecutedCommand = command;
            var respnose = executor.Do();
            Logger.Debug($"RESPONSE:\r\n{respnose}");

            return respnose;
        }

        #endregion
    }
}
