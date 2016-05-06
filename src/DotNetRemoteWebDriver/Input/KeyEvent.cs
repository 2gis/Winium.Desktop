#region using

using OpenQA.Selenium;

#endregion

namespace DotNetRemoteWebDriver.Input
{
    #region using

    

    #endregion

    internal class KeyEvent
    {
        #region Constructors and Destructors

        public KeyEvent(char ch)
        {
            character = ch;
            unicodeKey = KeyboardModifiers.GetKeyFromUnicode(character);
        }

        #endregion

        #region Fields

        private readonly char character;

        private readonly string unicodeKey;

        #endregion

        #region Public Methods and Operators

        public char GetCharacter()
        {
            return character;
        }

        public string GetKey()
        {
            return unicodeKey;
        }

        public bool IsModifier()
        {
            return KeyboardModifiers.IsModifier(unicodeKey);
        }

        public bool IsModifierRelease()
        {
            return GetKey() == Keys.Null;
        }

        public bool IsNewLine()
        {
            return GetCharacter() == '\n';
        }

        #endregion
    }
}