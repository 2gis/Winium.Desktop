namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;

    using Winium.StoreApps.Common;

    #endregion

    internal class FindChildElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var elementIds = this.Automator.Elements.FindElements(registeredKey, searchStrategy, searchValue);

            return this.JsonResponse(ResponseStatus.Success, elementIds.Select(e => new JsonWebElementContent(e)));
        }

        #endregion
    }
}
