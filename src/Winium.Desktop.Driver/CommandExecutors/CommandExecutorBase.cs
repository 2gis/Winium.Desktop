namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Net;

    using Newtonsoft.Json;

    using Winium.Desktop.Driver.Automator;
    using Winium.Desktop.Driver.Exceptions;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal abstract class CommandExecutorBase
    {
        #region Public Properties

        public Command ExecutedCommand { get; set; }

        #endregion

        #region Properties

        protected Automator Automator { get; set; }

        #endregion

        #region Public Methods and Operators

        public CommandResponse Do()
        {
            if (this.ExecutedCommand == null)
            {
                throw new NullReferenceException("ExecutedCommand property must be set before calling Do");
            }

            try
            {
                var session = this.ExecutedCommand.SessionId;
                this.Automator = Automator.InstanceForSession(session);
                return CommandResponse.Create(HttpStatusCode.OK, this.DoImpl());
            }
            catch (AutomationException exception)
            {
                return CommandResponse.Create(HttpStatusCode.OK, this.JsonResponse(exception.Status, exception));
            }
            catch (NotImplementedException exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.NotImplemented,
                    this.JsonResponse(ResponseStatus.UnknownCommand, exception));
            }
            catch (SessionNotCreatedException exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.InternalServerError,
                    this.JsonResponse(ResponseStatus.SessionNotCreatedException, exception.GetBaseException()));
            }
            catch (Exception exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.OK,
                    this.JsonResponse(ResponseStatus.UnknownError, exception));
            }
        }

        #endregion

        #region Methods

        protected abstract string DoImpl();

        /// <summary>
        /// The JsonResponse with SUCCESS status and NULL value.
        /// </summary>
        protected string JsonResponse()
        {
            return this.JsonResponse(ResponseStatus.Success, null);
        }

        protected string JsonResponse(ResponseStatus status, object value)
        {
            var session = this.Automator.Session;
            if (status == ResponseStatus.SessionNotCreatedException)
            {
                this.Automator.Session = null;
            }
            return JsonConvert.SerializeObject(
                new JsonResponse(session, status, value),
                Formatting.Indented);
        }

        #endregion
    }
}
