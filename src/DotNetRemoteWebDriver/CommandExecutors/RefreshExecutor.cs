namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class RefreshExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            Automator.Driver.Navigate().Refresh();
            return JsonResponse();
        }
    }
}