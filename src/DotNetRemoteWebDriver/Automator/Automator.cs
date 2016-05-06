using System;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;

namespace DotNetRemoteWebDriver.Automator
{
    internal class Automator : IDisposable
    {
        public Automator(string session)
        {
            Session = session;
            ElementsRegistry = new ElementsRegistry();
        }

        public ElementsRegistry ElementsRegistry { get; private set; }

        public string Session { get; private set; }

        public RemoteWebDriver Driver { get; set; }

        public static T GetValue<T>(IReadOnlyDictionary<string, object> parameters, string key) where T : class
        {
            object valueObject;
            parameters.TryGetValue(key, out valueObject);

            return valueObject as T;
        }

        private static readonly Dictionary<string, Automator> AliveSessions = new Dictionary<string, Automator>();

        public static Automator InstanceForSession(string sessionId)
        {
            Automator session;
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                session = new Automator(sessionId);
                AliveSessions.Add(sessionId, session);
                return session;
            }

            if (!AliveSessions.TryGetValue(sessionId, out session))
                throw new Exception("No active session with id: " + sessionId);

            return session;
        }

        public void Dispose()
        {
            Driver?.Dispose();
            Driver = null;
        }
    }
}