namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus;

    #endregion

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var text = string.Join(string.Empty, this.ExecutedCommand.Parameters["value"]);

            CruciatusFactory.Keyboard.SendText(text);

            return this.JsonResponse();
        }

        #endregion
    }
}
