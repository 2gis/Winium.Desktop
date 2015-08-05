namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class FindMenuItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = this.ExecutedCommand.Parameters["PATH"].ToString();

            var munu = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            var element = munu.GetItem(headersPath);
            if (element == null)
            {
                throw new AutomationException("No menu item was found", ResponseStatus.NoSuchElement);
            }

            var elementKey = this.Automator.ElementsRegistry.RegisterElement(element);

            return this.JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

        #endregion
    }
}
