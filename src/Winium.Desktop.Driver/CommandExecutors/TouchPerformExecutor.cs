namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows;

    using Winium.StoreApps.Common;
    using Newtonsoft.Json;

    using Winium.Cruciatus.Core;
    using Winium.Cruciatus.Elements;
    using Winium.Desktop.Driver.Automator;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class TouchPerformExecutor : CommandExecutorBase
    {

        #region Inner classes

        class JsonTouchAction
        {
            CruciatusElement element;

            [JsonProperty("action")]
            public string Action { get; set; }

            [JsonProperty("options")]
            public Dictionary<string, string> Options { get; set; }
        }

        class TouchAction
        {
            public const string LongPress = "longpress";
            public const string MoveTo = "moveto";
            public const string Press = "press";
            public const string Release = "release";
            public const string Tap = "tap";
            public const string Wait = "wait";

            JsonTouchAction _jsonTouchAction;

            Automator _automator;

            public TouchAction(JsonTouchAction jsonTouchAction, Automator automator)
            {
                _jsonTouchAction = jsonTouchAction;
                _automator = automator;

                if (this.Action == Wait)
                {
                    this.MiliSeconds = Convert.ToInt32(jsonTouchAction.Options["ms"]);
                    return;
                }

                if (this.Action == Release)
                {
                    return;
                }

                if (this.Action == LongPress)
                {
                    this.MiliSeconds = jsonTouchAction.Options.ContainsKey("duration")
                        ? Convert.ToInt32(jsonTouchAction.Options["duration"])
                        : 1000;
                }
            }

            public string Action => _jsonTouchAction.Action.ToLower();

            public Point GetLocation()
            {
                var point = new Point();

                if (_jsonTouchAction.Options.ContainsKey("element"))
                {
                    var element = _automator.ElementsRegistry.GetRegisteredElement(
                        _jsonTouchAction.Options["element"]);

                    var rect = element.Properties.BoundingRectangle;

                    point.X = _jsonTouchAction.Options.ContainsKey("x")
                                  ? rect.Left
                                  : (rect.Left + (rect.Width / 2));

                    point.Y = _jsonTouchAction.Options.ContainsKey("y")
                                  ? rect.Top
                                  : (rect.Top + (rect.Height / 2));
                }

                if (_jsonTouchAction.Options.ContainsKey("x"))
                {
                    point.X += Convert.ToInt32(_jsonTouchAction.Options["x"]);
                }

                if (_jsonTouchAction.Options.ContainsKey("y"))
                {
                    point.Y += Convert.ToInt32(_jsonTouchAction.Options["y"]);
                }

                return point;
            }

            public int MiliSeconds { get; }
        }

        #endregion

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

            foreach (var action in actions)
            {
                Point point;

                switch (action.Action)
                {
                    case TouchAction.LongPress:
                        point = action.GetLocation();
                        TouchSimulator.LongTap((int)point.X, (int)point.Y, action.MiliSeconds);
                        havePrevious = false;
                        break;
                    case TouchAction.MoveTo:
                        point = action.GetLocation();
                        TouchSimulator.TouchUpdate((int)point.X, (int)point.Y);
                        previousX = (int)point.X;
                        previousY = (int)point.Y;
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
                        TouchSimulator.Tap((int)point.X, (int)point.Y);
                        havePrevious = false;
                        break;
                    case TouchAction.Wait:
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