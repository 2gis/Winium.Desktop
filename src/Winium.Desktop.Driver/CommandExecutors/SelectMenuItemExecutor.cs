namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Exceptions;
    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class SelectMenuItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = this.ExecutedCommand.Parameters["PATH"].ToString();

            var menu = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            try
            {
                menu.SelectItem(headersPath);
            }
            catch (CruciatusException exception)
            {
                return this.JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
