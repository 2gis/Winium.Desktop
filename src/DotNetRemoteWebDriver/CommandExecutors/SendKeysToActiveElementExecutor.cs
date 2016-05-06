using OpenQA.Selenium.Interactions;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var keys = ExecutedCommand.Parameters["value"].ToString();
            var actions = new Actions(Automator.Driver);
            actions.SendKeys(keys);
            return JsonResponse();
        }
    }
}