#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotNetRemoteWebDriver.CommandExecutors;

#endregion

namespace DotNetRemoteWebDriver
{
    #region using

    

    #endregion

    internal class CommandExecutorDispatchTable
    {
        private static readonly string ExecutorsNamespace;

        static CommandExecutorDispatchTable()
        {
            ExecutorsNamespace = typeof (CommandExecutorBase).Namespace;
        }

        #region Fields

        private Dictionary<string, Type> commandExecutorsRepository;

        #endregion

        #region Constructors and Destructors

        public CommandExecutorDispatchTable()
        {
            ConstructDispatcherTable();
        }

        #endregion

        #region Public Methods and Operators

        public CommandExecutorBase GetExecutor(string commandName)
        {
            Type executorType;
            if (commandExecutorsRepository.TryGetValue(commandName, out executorType))
            {
            }
            else
            {
                executorType = typeof (NotImplementedExecutor);
            }

            return (CommandExecutorBase) Activator.CreateInstance(executorType);
        }

        #endregion

        #region Methods

        private void ConstructDispatcherTable()
        {
            commandExecutorsRepository = new Dictionary<string, Type>();

            var q =
                (from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == ExecutorsNamespace
                    select t).ToArray();

            var fields = typeof (DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var localField = field;
                var executor = q.FirstOrDefault(x => x.Name.Equals(localField.Name + "Executor"));
                if (executor != null)
                {
                    commandExecutorsRepository.Add(localField.GetValue(null).ToString(), executor);
                }
            }
        }

        #endregion
    }
}