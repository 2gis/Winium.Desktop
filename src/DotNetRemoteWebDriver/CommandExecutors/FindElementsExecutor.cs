#region using

using System.Linq;
using DotNetRemoteWebDriver.Extensions;
using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var searchValue = ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = ExecutedCommand.Parameters["using"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var elements = CruciatusFactory.Root.FindElements(strategy);

            var registeredKeys = Automator.ElementsRegistry.RegisterElements(elements);
            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return JsonResponse(ResponseStatus.Success, registeredObjects);
        }

        #endregion
    }
}