namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ClearElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            RequestedElement.Clear();
            return JsonResponse();
        }
    }
}