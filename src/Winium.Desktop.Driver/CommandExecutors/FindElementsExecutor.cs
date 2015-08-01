namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;

    using Winium.Cruciatus;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class FindElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var elements = CruciatusFactory.Root.FindElements(strategy);

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(elements);
            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

        #endregion
    }
}
