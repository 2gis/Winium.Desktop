namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class ClickElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).Click();

            return this.JsonResponse();
        }

        #endregion
    }
}
