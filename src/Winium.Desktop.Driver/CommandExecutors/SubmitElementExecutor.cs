namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus;

    #endregion

    internal class SubmitElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            CruciatusFactory.Keyboard.SendEnter();
            return this.JsonResponse();
        }

        #endregion
    }
}
