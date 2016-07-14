namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GoBackExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            Automator.Driver.Navigate().Back();
            return JsonResponse();
        }
    }
}