namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Core;
    using Winium.StoreApps.Common;

    #endregion

    internal class MouseClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var buttonId = Convert.ToInt32(this.ExecutedCommand.Parameters["button"]);

            switch ((MouseButton)buttonId)
            {
                case MouseButton.Left:
                    CruciatusFactory.Mouse.LeftButtonClick();
                    break;

                case MouseButton.Right:
                    CruciatusFactory.Mouse.RightButtonClick();
                    break;

                default:
                    return this.JsonResponse(ResponseStatus.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
