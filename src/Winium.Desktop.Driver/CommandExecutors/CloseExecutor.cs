namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.CommonHelpers;

    #endregion

    internal class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return CommonHelpers.TerminateExcecutor(this.Automator, this.JsonResponse());
        }

        #endregion
    }
}
