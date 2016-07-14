namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class QuitExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            Automator.Driver.Quit();
            return JsonResponse();
        }
    }
}