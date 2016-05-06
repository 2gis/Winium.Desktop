#region using

using System.Linq;
using DotNetRemoteWebDriver.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindChildElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = ExecutedCommand.Parameters["using"].ToString();

            var parent = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var elements = parent.FindElements(strategy);

            var registeredKeys = Automator.ElementsRegistry.RegisterElements(elements);
            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return JsonResponse(ResponseStatus.Success, registeredObjects);
        }

        #endregion
    }
}