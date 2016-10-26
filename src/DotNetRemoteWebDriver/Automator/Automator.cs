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

        public static void Clear()
        {
            foreach (var aliveSession in AliveSessions)
            {
                try
                {
                    aliveSession.Value.Dispose();
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Failed to clear out old driver session: " + e.Message, e);
                }
            }

            AliveSessions.Clear();
        }

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
            Driver?.Quit();
            Driver?.Dispose();
            Driver = null;
        }
    }
}