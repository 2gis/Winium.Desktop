namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;

    using Winium.Cruciatus;
    using Winium.StoreApps.Common;

    #endregion

    internal class FindElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var elementIds = this.Automator.Elements.FindElements(CruciatusFactory.Root, searchStrategy, searchValue);

            return this.JsonResponse(ResponseStatus.Success, elementIds.Select(e => new JsonWebElementContent(e)));
        }

        #endregion
    }
}
