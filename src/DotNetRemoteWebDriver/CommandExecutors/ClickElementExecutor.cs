namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ClickElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            RequestedElement.Click();
            return JsonResponse();
        }
    }
}