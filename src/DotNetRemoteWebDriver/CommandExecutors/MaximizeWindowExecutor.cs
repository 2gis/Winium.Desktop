namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class MaximizeWindowExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            Automator.Driver.Manage().Window.Maximize();
            return JsonResponse();
        }
    }
}