using DotNetRemoteWebDriver.Exceptions;
using OpenQA.Selenium;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class FindChildElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var element = RequestedElement.FindElement(Identifier.From(ExecutedCommand.Parameters));
            if (element == null)
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);

            var registeredKey = Automator.ElementsRegistry.Register(element);
            var registeredObject = new JsonElementContent(registeredKey);
            return JsonResponse(ResponseStatus.Success, registeredObject);
        }
    }
}