namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            Automator.Driver.Quit();
            return JsonResponse();
        }

        #endregion
    }
}