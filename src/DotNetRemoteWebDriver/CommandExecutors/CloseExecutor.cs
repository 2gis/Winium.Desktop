namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            Automator.Driver.Close();
            Automator.ElementsRegistry.Clear();
            return JsonResponse();
        }
    }
}