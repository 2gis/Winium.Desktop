#region using

using System.Windows.Automation;
using DotNetRemoteWebDriver.Exceptions;
using Winium.Cruciatus;
using Winium.Cruciatus.Core;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class SwitchToWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var windowHandle = int.Parse(ExecutedCommand.Parameters["name"].ToString());

            var handleProperty = AutomationElement.NativeWindowHandleProperty;
            var window = CruciatusFactory.Root.FindElement(By.AutomationProperty(handleProperty, windowHandle));
            if (window == null)
            {
                throw new AutomationException("Window cannot be found", ResponseStatus.NoSuchElement);
            }

            window.SetFocus();

            return JsonResponse();
        }

        #endregion
    }
}