namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GoForwardExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            Automator.Driver.Navigate().Forward();
            return JsonResponse();
        }
    }
}
