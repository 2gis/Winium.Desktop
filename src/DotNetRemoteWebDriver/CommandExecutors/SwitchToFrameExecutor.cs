namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SwitchToFrameExecutor : CommandExecutorBase
    {
        private string _name;
        private bool _isElement;
        private string _elementKey;
        private bool _switchToDefaultContent;

        protected override string DoImpl()
        {
            ReadParameters();

            if (_switchToDefaultContent)
                Automator.Driver.SwitchTo().DefaultContent();
            else if (_isElement)
                Automator.Driver.SwitchTo().Frame(Automator.ElementsRegistry.Get(_elementKey));
            else
                Automator.Driver.SwitchTo().Frame(_name);

            return JsonResponse();
        }

        private void ReadParameters()
        {
            var name = ExecutedCommand.Parameters["name"]?.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                _name = name;
                return;
            }

            var idParameter = ExecutedCommand.Parameters["id"];
            if (string.IsNullOrEmpty(idParameter.ToString()))
            {
                _switchToDefaultContent = true;
                return;
            }

            if (idParameter.HasValues)
            {
                _elementKey = idParameter["ELEMENT"].ToString();
                _isElement = true;
                return;
            }
            _name = idParameter.ToString();
        }
    }
}