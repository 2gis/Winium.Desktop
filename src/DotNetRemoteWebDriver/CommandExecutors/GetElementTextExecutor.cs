namespace DotNetRemoteWebDriver.CommandExecutors
{

    #region using

    #endregion

    internal class GetElementTextExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return JsonResponse(ResponseStatus.Success, element.Text());
        }

        #endregion
    }
}