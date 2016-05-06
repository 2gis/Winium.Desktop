#region using

using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class GetActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = Automator.ElementsRegistry.RegisterElement(CruciatusFactory.FocusedElement);
            var registeredObject = new JsonElementContent(registeredKey);
            return JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}