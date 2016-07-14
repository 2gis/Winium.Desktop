using System;
using OpenQA.Selenium.Interactions;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class MouseClickExecutor : CommandExecutorBase
    {
        private const int LeftButton = 0;
        private const int RightButton = 2;

        protected override string DoImpl()
        {
            var actions = new Actions(Automator.Driver);
            var buttonId = Convert.ToInt32(ExecutedCommand.Parameters["button"]);
            var element = IsElementRequest ? RequestedElement : null;
            if (buttonId == LeftButton)
                actions.Click(element);
            else if (buttonId == RightButton)
                actions.ContextClick(element);
            else
                return JsonResponse(ResponseStatus.UnknownCommand, "No idea what this kind of button is: " + buttonId);

            actions.Perform();
            return JsonResponse();
        }
    }
}