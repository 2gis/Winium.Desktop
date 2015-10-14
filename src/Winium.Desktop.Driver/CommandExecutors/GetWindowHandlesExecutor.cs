namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;
    using System.Windows.Automation;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Core;
    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetWindowHandlesExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var typeProperty = AutomationElement.ControlTypeProperty;
            var windows = CruciatusFactory.Root.FindElements(By.AutomationProperty(typeProperty, ControlType.Window));

            var handleProperty = AutomationElement.NativeWindowHandleProperty;
            var handles = windows.Select(element => element.GetAutomationPropertyValue<int>(handleProperty));

            return this.JsonResponse(ResponseStatus.Success, handles);
        }

        #endregion
    }
}
