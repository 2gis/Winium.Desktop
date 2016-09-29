namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.CommandHelpers;

    #endregion

    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return TerminateApp.TerminateExcecutor(this.Automator, this.JsonResponse());
        }

        #endregion
    }
}
