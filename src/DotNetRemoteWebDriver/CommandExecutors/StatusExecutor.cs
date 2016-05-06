namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    using System.Collections.Generic;

    using DotNetRemoteWebDriver.CommandHelpers;

    #endregion

    internal class StatusExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var response = new Dictionary<string, object> { { "build", new BuildInfo() }, { "os", new OSInfo() } };
            return this.JsonResponse(ResponseStatus.Success, response);
        }

        #endregion
    }
}
