#region using

using Winium.Cruciatus;
using Winium.Cruciatus.Core;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            CruciatusFactory.Mouse.DoubleClick(MouseButton.Left);
            return JsonResponse();
        }

        #endregion
    }
}