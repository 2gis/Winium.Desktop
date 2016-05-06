﻿#region using

using System;
using System.Net;
using DotNetRemoteWebDriver.Exceptions;
using Newtonsoft.Json;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal abstract class CommandExecutorBase
    {
        #region Public Properties

        public Command ExecutedCommand { get; set; }

        #endregion

        #region Properties

        protected Automator.Automator Automator { get; set; }

        #endregion

        #region Public Methods and Operators

        public CommandResponse Do()
        {
            if (ExecutedCommand == null)
            {
                throw new NullReferenceException("ExecutedCommand property must be set before calling Do");
            }

            try
            {
                var session = ExecutedCommand.SessionId;
                Automator = DotNetRemoteWebDriver.Automator.Automator.InstanceForSession(session);
                return CommandResponse.Create(HttpStatusCode.OK, DoImpl());
            }
            catch (AutomationException exception)
            {
                return CommandResponse.Create(HttpStatusCode.OK, JsonResponse(exception.Status, exception));
            }
            catch (NotImplementedException exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.NotImplemented,
                    JsonResponse(ResponseStatus.UnknownCommand, exception));
            }
            catch (Exception exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.OK,
                    JsonResponse(ResponseStatus.UnknownError, exception));
            }
        }

        #endregion

        #region Methods

        protected abstract string DoImpl();

        /// <summary>
        ///     The JsonResponse with SUCCESS status and NULL value.
        /// </summary>
        protected string JsonResponse()
        {
            return JsonResponse(ResponseStatus.Success, null);
        }

        protected string JsonResponse(ResponseStatus status, object value)
        {
            return JsonConvert.SerializeObject(
                new JsonResponse(Automator.Session, status, value),
                Formatting.Indented);
        }

        #endregion
    }
}