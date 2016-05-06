namespace DotNetRemoteWebDriver.CommandExecutors
{

    #region using

    #endregion

    internal class IsElementEnabledExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return JsonResponse(ResponseStatus.Success, element.Properties.IsEnabled);
        }

        #endregion
    }
}