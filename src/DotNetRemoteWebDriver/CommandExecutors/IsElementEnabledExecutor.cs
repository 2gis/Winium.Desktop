namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class IsElementEnabledExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, element.Properties.IsEnabled);
        }

        #endregion
    }
}
