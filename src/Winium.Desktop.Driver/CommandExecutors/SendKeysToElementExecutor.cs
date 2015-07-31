namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SendKeysToElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var text = string.Join(string.Empty, this.ExecutedCommand.Parameters["value"]);

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            element.SetText(text);

            return this.JsonResponse();
        }

        #endregion
    }
}
