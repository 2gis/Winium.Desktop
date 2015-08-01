namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!this.Automator.ActualCapabilities.DebugConnectToRunningApp)
            {
                if (!this.Automator.Application.Close())
                {
                    this.Automator.Application.Kill();
                }

                this.Automator.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
