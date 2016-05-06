namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            this.Automator.Driver.Quit();
            return this.JsonResponse();
        }

        #endregion
    }
}
