using OpenQA.Selenium.Interactions;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class MouseMoveToExecutor : CommandExecutorBase
    {
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

            var offset = haveOffset
                ? new[]
                {
                    int.Parse(ExecutedCommand.Parameters.ContainsKey("xoffset").ToString()),
                    int.Parse(ExecutedCommand.Parameters.ContainsKey("yoffset").ToString())
                }
                : new[] {0, 0};

            var element = haveElement
                ? Automator.ElementsRegistry.Get(ExecutedCommand.Parameters["element"].ToString())
                : null;

            var position = new[] {0, 0};

            if (haveElement)
            {
                var location = element.Location;
                var middle = new[] {(int) (element.Size.Width*0.5), (int) (element.Size.Height*0.5)};
                position[0] += location.X + middle[0];
                position[1] += location.Y + middle[1];
            }

            var actions = new Actions(Automator.Driver);
            if (haveElement)
                actions.MoveToElement(element, offset[0], offset[1]);
            else
                actions.MoveByOffset(offset[0], offset[1]);

            actions.Perform();

            return JsonResponse();
        }
    }
}