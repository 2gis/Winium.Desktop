namespace Winium.Desktop.Driver.CommandHelpers
{
    #region using

    using System;
    using System.Windows;

    using Winium.Desktop.Driver.Automator;

    #endregion

    class TouchAction
    {
        #region Fields

        JsonTouchAction _jsonTouchAction;

        Automator _automator;

        #endregion

        #region Public Constants
        
        public const string LongPress = "longpress";
        public const string MoveTo = "moveto";
        public const string Press = "press";
        public const string Release = "release";
        public const string Tap = "tap";
        public const string Wait = "wait";

        #endregion

        #region Public Properties

        public string Action => _jsonTouchAction.Action.ToLower();

        public int MiliSeconds { get; }

        public int Count { get; }

        #endregion

        #region Public Methods

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

            if (this.Action == Tap)
            {
                int count;
                this.Count = int.TryParse(jsonTouchAction.Options["count"], out count) ? count : 1;
            }

            if (this.Action == LongPress)
            {
                this.MiliSeconds = jsonTouchAction.Options.ContainsKey("duration")
                    ? Convert.ToInt32(jsonTouchAction.Options["duration"])
                    : 1000;
            }
        }

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

        #endregion
    }
}
