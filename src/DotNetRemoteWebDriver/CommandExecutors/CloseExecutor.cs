namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!Automator.ActualCapabilities.DebugConnectToRunningApp)
            {
                if (!Automator.Application.Close())
                {
                    Automator.Application.Kill();
                }

                Automator.ElementsRegistry.Clear();
            }

            return JsonResponse();
        }

        #endregion
    }
}