using System.Linq;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class FindElementsExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var elements = Automator.Driver.FindElements(Identifier.From(ExecutedCommand.Parameters));
            var registeredKeys = elements.Select(Automator.ElementsRegistry.Register);
            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return JsonResponse(ResponseStatus.Success, registeredObjects);
        }
    }
}