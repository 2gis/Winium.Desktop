using DotNetRemoteWebDriver.Exceptions;
using OpenQA.Selenium;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class FindChildElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            try
            {
                var element = RequestedElement.FindElement(Identifier.From(ExecutedCommand.Parameters));
                var registeredKey = Automator.ElementsRegistry.Register(element);
                var registeredObject = new JsonElementContent(registeredKey);
                return JsonResponse(ResponseStatus.Success, registeredObject);
            }
            catch (NoSuchElementException e)
            {
                throw new AutomationException(e.Message, ResponseStatus.NoSuchElement);
            }
        }
    }
}