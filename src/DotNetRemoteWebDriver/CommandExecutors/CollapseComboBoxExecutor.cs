#region using

using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class CollapseComboBoxExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Collapse();

            return JsonResponse();
        }

        #endregion
    }
}