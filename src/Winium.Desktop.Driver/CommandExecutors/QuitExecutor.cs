namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            this.Automator.CloseApplication();
            return this.JsonResponse();
        }

        #endregion
    }
}
