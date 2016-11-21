using DotNetRemoteWebDriver.Exceptions;
using OpenQA.Selenium;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class FindElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            IWebElement element;
            try
            {
                element = Automator.Driver.FindElement(Identifier.From(ExecutedCommand.Parameters));
            }
            catch (NoSuchElementException e)
            {
                throw new AutomationException(e.Message, ResponseStatus.NoSuchElement);
            }    

            var registeredKey = Automator.ElementsRegistry.Register(element);
            var registeredObject = new JsonElementContent(registeredKey);
            return JsonResponse(ResponseStatus.Success, registeredObject);
        }
    }
}