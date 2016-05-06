#region using

using System;
using Winium.Cruciatus;
using Winium.Cruciatus.Core;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class MouseClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var buttonId = Convert.ToInt32(ExecutedCommand.Parameters["button"]);

            switch ((MouseButton) buttonId)
            {
                case MouseButton.Left:
                    CruciatusFactory.Mouse.LeftButtonClick();
                    break;

                case MouseButton.Right:
                    CruciatusFactory.Mouse.RightButtonClick();
                    break;

                default:
                    return JsonResponse(ResponseStatus.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return JsonResponse();
        }

        #endregion
    }
}