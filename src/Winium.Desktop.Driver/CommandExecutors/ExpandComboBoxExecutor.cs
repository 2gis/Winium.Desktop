namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Core;
    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class ExpandComboBoxExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Expand();

            return this.JsonResponse();
        }

        #endregion
    }
}
