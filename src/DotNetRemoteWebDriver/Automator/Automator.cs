namespace DotNetRemoteWebDriver.Automator
{
    #region using

    using System;
    using System.Collections.Generic;

    using DotNetRemoteWebDriver.Input;

    using OpenQA.Selenium.Remote;

    using Winium.Cruciatus;

    #endregion

    internal class Automator
    {
        #region Static Fields

        private static readonly object LockObject = new object();

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

        public RemoteWebDriver Driver { get; set; }

        #endregion

        #region Public Methods and Operators

        public static T GetValue<T>(IReadOnlyDictionary<string, object> parameters, string key) where T : class
        {
            object valueObject;
            parameters.TryGetValue(key, out valueObject);

            return valueObject as T;
        }

        private readonly static Dictionary<string, Automator> _aliveSessions = new Dictionary<string, Automator>();

        public static Automator InstanceForSession(string sessionId)
        {
            Automator session;
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                session = new Automator(sessionId);
                _aliveSessions.Add(sessionId, session);
                return session;
            }

            if(!_aliveSessions.TryGetValue(sessionId, out session))
                throw new Exception("No active session with id: " + sessionId);

            return session;
        }

        #endregion
    }
}
