namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetWindowHandlesExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var handles = Automator.Driver.WindowHandles;
            return JsonResponse(ResponseStatus.Success, handles);
        }
    }
}