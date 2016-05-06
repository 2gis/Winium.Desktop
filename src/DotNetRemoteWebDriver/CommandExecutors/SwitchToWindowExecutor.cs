namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    using System.Windows.Automation;

    using DotNetRemoteWebDriver.Exceptions;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Core;

    #endregion

    internal class SwitchToWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var windowHandle = int.Parse(this.ExecutedCommand.Parameters["name"].ToString());

            var handleProperty = AutomationElement.NativeWindowHandleProperty;
            var window = CruciatusFactory.Root.FindElement(By.AutomationProperty(handleProperty, windowHandle));
            if (window == null)
            {
                throw new AutomationException("Window cannot be found", ResponseStatus.NoSuchElement);
            }

            window.SetFocus();

            return this.JsonResponse();
        }

        #endregion
    }
}
