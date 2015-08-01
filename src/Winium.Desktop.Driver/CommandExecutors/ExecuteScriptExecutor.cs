namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    using Newtonsoft.Json.Linq;

    using Winium.Cruciatus.Elements;
    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        #region Constants

        internal const string HelpArgumentsErrorMsg = "Arguments error. See {0} for more information.";

        internal const string HelpUnknownScriptMsg = "Unknown script command '{0} {1}'. See {2} for supported commands.";

        internal const string HelpUrlAutomationScript =
            "https://github.com/2gis/Winium.Desktop/wiki/Command-Execute-Script#use-ui-automation-patterns-on-element";

        internal const string HelpUrlInputScript =
            "https://github.com/2gis/Winium.Desktop/wiki/Command-Execute-Script#simulate-input";

        internal const string HelpUrlScript = "https://github.com/2gis/Winium.Desktop/wiki/Command-Execute-Script";

        #endregion

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
                case "automation":
                    this.ExecuteAutomationScript(command);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, prefix, command, HelpUrlScript);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            return this.JsonResponse();
        }

        private void ExecuteAutomationScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            switch (command)
            {
                case "ValuePattern.SetValue":
                    this.ValuePatternSetValue(element, args);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "automation:", command, HelpUrlAutomationScript);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ExecuteInputScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            switch (command)
            {
                case "ctrl_click":
                    element.ClickWithPressedCtrl();
                    return;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "input:", command, HelpUrlInputScript);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ValuePatternSetValue(CruciatusElement element, IEnumerable<JToken> args)
        {
            var value = args.ElementAtOrDefault(1);
            if (value == null)
            {
                var msg = string.Format(HelpArgumentsErrorMsg, HelpUrlAutomationScript);
                throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            element.GetPattern<ValuePattern>(ValuePattern.Pattern).SetValue(value.ToString());
        }

        #endregion
    }
}
