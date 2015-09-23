namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Globalization;
    using System.Windows.Automation;

    using Winium.StoreApps.Common;

    #endregion

    internal class GetCurrentWindowHandleExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var node = AutomationElement.FocusedElement;
            var rootElement = AutomationElement.RootElement;
            var treeWalker = TreeWalker.ControlViewWalker;
            while (node != rootElement && !node.Current.ControlType.Equals(ControlType.Window))
            {
                node = treeWalker.GetParent(node);
            }

            var result = (node == rootElement)
                             ? string.Empty
                             : node.Current.NativeWindowHandle.ToString(CultureInfo.InvariantCulture);
            return this.JsonResponse(ResponseStatus.Success, result);
        }

        #endregion
    }
}
