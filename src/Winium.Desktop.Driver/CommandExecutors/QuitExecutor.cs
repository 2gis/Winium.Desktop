namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.CommonHelpers;

    #endregion

    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return CommonHelpers.TerminateExcecutor(this.Automator, this.JsonResponse());
        }

        #endregion
    }
}
