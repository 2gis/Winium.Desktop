namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus;
    using Winium.StoreApps.Common;

    #endregion

    internal class MouseMoveToExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var haveElement = this.ExecutedCommand.Parameters.ContainsKey("element");
            var haveOffset = this.ExecutedCommand.Parameters.ContainsKey("xoffset")
                             && this.ExecutedCommand.Parameters.ContainsKey("yoffset");

            if (!(haveElement || haveOffset))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var resultPoint = CruciatusFactory.Mouse.CurrentCursorPos;
            if (haveElement)
            {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                var element = this.Automator.ElementsRegistry.GetRegisteredElementOrNull(registeredKey);
                if (element != null)
                {
                    var rect = element.Properties.BoundingRectangle;
                    resultPoint.X = rect.TopLeft.X;
                    resultPoint.Y = rect.TopLeft.Y;
                    if (!haveOffset)
                    {
                        resultPoint.X += rect.Width / 2;
                        resultPoint.Y += rect.Height / 2;
                    }
                }
            }

            if (haveOffset)
            {
                resultPoint.X += Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"]);
                resultPoint.Y += Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"]);
            }

            CruciatusFactory.Mouse.SetCursorPos(resultPoint.X, resultPoint.Y);

            return this.JsonResponse();
        }

        #endregion
    }
}
