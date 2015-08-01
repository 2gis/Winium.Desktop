namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class ClearElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            element.SetText(null);

            return this.JsonResponse();
        }

        #endregion
    }
}
