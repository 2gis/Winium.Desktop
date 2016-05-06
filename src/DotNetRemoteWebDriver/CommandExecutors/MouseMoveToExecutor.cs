#region using

using System;
using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class MouseMoveToExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var haveElement = ExecutedCommand.Parameters.ContainsKey("element");
            var haveOffset = ExecutedCommand.Parameters.ContainsKey("xoffset")
                             && ExecutedCommand.Parameters.ContainsKey("yoffset");

            if (!(haveElement || haveOffset))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var resultPoint = CruciatusFactory.Mouse.CurrentCursorPos;
            if (haveElement)
            {
                var registeredKey = ExecutedCommand.Parameters["element"].ToString();
                var element = Automator.ElementsRegistry.GetRegisteredElementOrNull(registeredKey);
                if (element != null)
                {
                    var rect = element.Properties.BoundingRectangle;
                    resultPoint.X = rect.TopLeft.X;
                    resultPoint.Y = rect.TopLeft.Y;
                    if (!haveOffset)
                    {
                        resultPoint.X += rect.Width/2;
                        resultPoint.Y += rect.Height/2;
                    }
                }
            }

            if (haveOffset)
            {
                resultPoint.X += Convert.ToInt32(ExecutedCommand.Parameters["xoffset"]);
                resultPoint.Y += Convert.ToInt32(ExecutedCommand.Parameters["yoffset"]);
            }

            CruciatusFactory.Mouse.SetCursorPos(resultPoint.X, resultPoint.Y);

            return JsonResponse();
        }

        #endregion
    }
}