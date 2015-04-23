namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Newtonsoft.Json.Linq;

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var script = this.ExecutedCommand.Parameters["script"].ToString();

            var prefix = string.Empty;
            string command;

            var index = script.IndexOf(':');
            if (index == -1)
            {
                command = script;
            }
            else
            {
                prefix = script.Substring(0, index);
                command = script.Substring(++index).Trim();
            }

            switch (prefix)
            {
                case "input":
                    this.ExecuteInputScript(command);
                    break;
                default:
                    var msg = string.Format("Unknown script command '{0} {1}'", prefix, command);
                    return this.JsonResponse(ResponseStatus.JavaScriptError, msg);
            }

            return this.JsonResponse();
        }

        private void ExecuteInputScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automator.Elements.GetRegisteredElement(elementId);

            switch (command)
            {
                case "ctrl_click":
                    element.ClickWithPressedCtrl();
                    return;
                default:
                    throw new NotImplementedException(string.Format("Input-command {0} didn't implemented", command));
            }
        }

        #endregion
    }
}
