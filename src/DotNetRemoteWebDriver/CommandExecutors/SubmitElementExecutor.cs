namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SubmitElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            if (IsElementRequest)
                RequestedElement.Submit();
            else
                Automator.Driver.Keyboard.SendKeys("{ENTER}");
            
            return JsonResponse();
        }
    }
}