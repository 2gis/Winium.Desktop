namespace DotNetRemoteWebDriver.CommandExecutors
{

    #region using

    #endregion

    internal class ElementEqualsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            var otherRegisteredKey = ExecutedCommand.Parameters["other"].ToString();

            return JsonResponse(ResponseStatus.Success, registeredKey == otherRegisteredKey);
        }

        #endregion
    }
}