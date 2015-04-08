namespace Winium.Desktop.Driver.CommandExecutors
{
    using Winium.Cruciatus;

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var text = string.Join("", this.ExecutedCommand.Parameters["value"]);

            CruciatusFactory.Keyboard.SendText(text);

            return this.JsonResponse();
        }

        #endregion
    }
}
