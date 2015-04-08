namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SendKeysToElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var text = this.ExecutedCommand.Parameters["value"].First.ToString();

            var element = this.Automator.Elements.GetRegisteredElement(registeredKey);
            element.SetText(text);

            return this.JsonResponse();
        }

        #endregion

    }
}
