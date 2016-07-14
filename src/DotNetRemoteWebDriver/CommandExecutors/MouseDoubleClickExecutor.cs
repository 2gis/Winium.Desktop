using OpenQA.Selenium.Interactions;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            new Actions(Automator.Driver).DoubleClick().Perform();
            return JsonResponse();
        }
    }
}