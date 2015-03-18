namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus;
    using Winium.StoreApps.Common;

    #endregion

    internal class FindElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var elementId = this.Automator.Elements.FindElement(CruciatusFactory.Root, searchStrategy, searchValue);
            var webElement = new JsonWebElementContent(elementId);
            return this.JsonResponse(ResponseStatus.Success, webElement);
        }

        #endregion
    }
}
