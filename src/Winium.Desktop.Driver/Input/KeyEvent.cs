namespace Winium.Desktop.Driver.Input
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenQA.Selenium;

    #endregion

    class KeyEvent
    {
        #region Private Fields

        private char character;
        private string unicodeKey;

        #endregion

        #region Public Methods and Operators

        public KeyEvent(char ch)
        {
            this.character = ch;
            this.unicodeKey = KeyboardModifiers.GetKeyFromUnicode(this.character);
        }

        public string GetKey()
        {
            return this.unicodeKey;
        }

        public char GetCharacter()
        {
            return this.character;
        }

        public bool IsModifier()
        {
            return KeyboardModifiers.IsModifier(this.unicodeKey);
        }
       
        public bool IsModifierRelease()
        {
            return this.GetKey() == Keys.Null;
        }
        
        public bool IsNewLine()
        {
            return this.GetCharacter() == '\n';
        }

        #endregion
    }
}
