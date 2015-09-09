namespace Winium.Desktop.Driver.Automator
{
    #region using

    using System;
    using System.Collections.Generic;

    using Winium.Cruciatus;
    using Winium.Desktop.Driver.Input;

    #endregion

    internal class Automator
    {
        #region Static Fields

        private static readonly object LockObject = new object();

        private static Dictionary<string, Automator> automators = new Dictionary<string, Automator>();

        public static IEnumerable<Automator> Automators { get { return automators.Values; } }

        #endregion

        #region Constructors and Destructors

        public Automator(string session)
        {
            this.Session = session;
            this.ElementsRegistry = new ElementsRegistry();
        }

        #endregion

        #region Public Properties

        public Capabilities ActualCapabilities { get; set; }

        public Application Application { get; set; }

        public ElementsRegistry ElementsRegistry { get; private set; }

        public string Session { get; private set; }

        public WiniumKeyboard WiniumKeyboard { get; set; }

        #endregion

        #region Public Methods and Operators

        public void CloseApplication()
        {
            if (!ActualCapabilities.DebugConnectToRunningApp)
            {
                if (!Application.Close())
                {
                    Application.Kill();
                }

                ElementsRegistry.Clear();
            }

            lock (LockObject)
            {
                automators.Remove(Session);
            }
        }

        public static T GetValue<T>(IReadOnlyDictionary<string, object> parameters, string key) where T : class
        {
            object valueObject;
            parameters.TryGetValue(key, out valueObject);

            return valueObject as T;
        }

        public static Automator InstanceForSession(string sessionId)
        {
            if (sessionId == null)
                sessionId = Guid.NewGuid().ToString();

            if (automators.ContainsKey(sessionId))
                return automators[sessionId];

            lock (LockObject)
            {
                var newAutomator = new Automator(sessionId);
                automators.Add(sessionId, newAutomator);
                return newAutomator;
            }
        }

        #endregion
    }
}
