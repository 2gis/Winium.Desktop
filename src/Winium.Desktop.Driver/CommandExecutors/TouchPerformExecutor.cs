namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows;

    using Winium.StoreApps.Common;
    using Winium.Cruciatus.Core;
    using Winium.Desktop.Driver.CommandHelpers;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class TouchPerformExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!this.ExecutedCommand.Parameters.ContainsKey("actions"))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var actions = this.ExecutedCommand.Parameters["actions"]
                .ToObject<List<JsonTouchAction>>()
                .Select(a => new TouchAction(a, this.Automator))
                .ToList();

            var success = false;

            if (actions.Count == 4 
                && actions[0].Action == TouchAction.Press
                && actions[1].Action == TouchAction.Wait
                && actions[2].Action == TouchAction.MoveTo
                && actions[3].Action == TouchAction.Release)
            {
                success = Flick(actions);
            }
            else if (actions.Count == 5
                && actions[0].Action == TouchAction.Press
                && actions[1].Action == TouchAction.Wait
                && actions[2].Action == TouchAction.MoveTo 
                && actions[3].Action == TouchAction.Wait
                && actions[4].Action == TouchAction.Release)
            {
                success = DragWithTimes(actions);
            }
            else if (actions.Count == 3
                && actions[0].Action == TouchAction.LongPress
                && actions[1].Action == TouchAction.MoveTo
                && actions[2].Action == TouchAction.Release)
            {
                success = Drag(actions);
            }
            else
            {
                success = Perform(actions);
            }

            return success
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        private static bool Flick(List<TouchAction> actions)
        {
            var startPoint = actions[0].GetLocation();
            var endPoint = actions[2].GetLocation();

            return TouchSimulator.Flick(
                (int)startPoint.X,
                (int)startPoint.Y,
                (int)endPoint.X,
                (int)endPoint.Y,
                actions[1].MiliSeconds);
        }

        private static bool DragWithTimes(List<TouchAction> actions)
        {
            var startPoint = actions[0].GetLocation();
            var endPoint = actions[2].GetLocation();

            return TouchSimulator.Scroll(
                (int)startPoint.X,
                (int)startPoint.Y,
                (int)endPoint.X,
                (int)endPoint.Y,
                actions[1].MiliSeconds,
                actions[3].MiliSeconds);
        }

        private static bool Drag(List<TouchAction> actions)
        {
            var startPoint = actions[0].GetLocation();
            var endPoint = actions[1].GetLocation();

            return TouchSimulator.Scroll(
                (int)startPoint.X,
                (int)startPoint.Y,
                (int)endPoint.X,
                (int)endPoint.Y);
        }

        private static bool Perform(List<TouchAction> actions)
        {
            var previousX = 0;
            var previousY = 0;
            var havePrevious = false;
            string previousAction = null;

            for (var i = 0; i < actions.Count; i ++)
            {
                var action = actions[i];

                Point point;

                switch (action.Action)
                {
                    case TouchAction.LongPress:
                        point = action.GetLocation();
                        TouchSimulator.LongTap((int)point.X, (int)point.Y, action.MiliSeconds);
                        havePrevious = false;
                        break;
                    case TouchAction.MoveTo:
                        int? duration = null;
                        if (i > 0 && actions[i - 1].Action == TouchAction.Wait)
                        {
                            duration = actions[i - 1].MiliSeconds;
                        }
                        point = action.GetLocation();
                        var x = (int)point.X;
                        var y = (int)point.Y;
                        TouchSimulator.MoveTo(previousX, previousY, previousX + x, previousY + y, duration);
                        previousX += x;
                        previousY += y;
                        havePrevious = true;
                        break;
                    case TouchAction.Press:
                        point = action.GetLocation();
                        TouchSimulator.TouchDown((int)point.X, (int)point.Y);
                        previousX = (int)point.X;
                        previousY = (int)point.Y;
                        havePrevious = true;
                        break;
                    case TouchAction.Release:
                        if (previousAction == TouchAction.Tap || previousAction == TouchAction.LongPress)
                        {
                            break;
                        }
                        TouchSimulator.TouchUp(previousX, previousY);
                        havePrevious = false;
                        break;
                    case TouchAction.Tap:
                        point = action.GetLocation();
                        for (var n = 1; n <= action.Count; n++)
                        {
                            TouchSimulator.Tap((int)point.X, (int)point.Y);
                            Thread.Sleep(250);
                        }
                        havePrevious = false;
                        break;
                    case TouchAction.Wait:
                        if (actions.Count > i + 1 && actions[i + 1].Action == TouchAction.MoveTo)
                        {
                            break;
                        }
                        if (havePrevious)
                        {
                            var startTime = DateTime.Now;
                            while (DateTime.Now < startTime + TimeSpan.FromMilliseconds(action.MiliSeconds))
                            {
                                TouchSimulator.TouchUpdate(previousX, previousY);
                                Thread.Sleep(16);
                            }
                        }
                        else
                        {
                            Thread.Sleep(action.MiliSeconds);
                        }
                        break;
                    default:
                        throw new AutomationException($"unrecognised action {action.Action}");
                }

                previousAction = action.Action;
            }

            return true;
        }

        #endregion
    }
}