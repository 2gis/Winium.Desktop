namespace Winium.Desktop.Driver.Input
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OpenQA.Selenium;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Settings;

    #endregion

    internal class WiniumKeyboard
    {
        #region Fields

        private readonly KeyboardModifiers modifiers = new KeyboardModifiers();

        #endregion

        #region Constructors and Destructors

        public WiniumKeyboard(KeyboardSimulatorType keyboardSimulatorType)
        {
            CruciatusFactory.Settings.KeyboardSimulatorType = keyboardSimulatorType;
        }

        #endregion

        #region Public Methods and Operators

        public void KeyDown(string keyToPress)
        {
            var key = KeyboardModifiers.GetVirtualKeyCode(keyToPress);
            this.modifiers.Add(keyToPress);
            CruciatusFactory.Keyboard.KeyDown(key);
        }

        public void KeyUp(string keyToRelease)
        {
            var key = KeyboardModifiers.GetVirtualKeyCode(keyToRelease);
            this.modifiers.Remove(keyToRelease);
            CruciatusFactory.Keyboard.KeyUp(key);
        }

        public void SendKeys(char[] keysToSend)
        {
            var builder = keysToSend.Select(key => new KeyEvent(key)).ToList();

            this.SendKeys(builder);
        }

        #endregion

        #region Methods

        protected void ReleaseModifiers()
        {
            var tmp = this.modifiers.ToList();

            foreach (var modifierKey in tmp)
            {
                this.KeyUp(modifierKey);
            }
        }

        private void PressOrReleaseModifier(string modifier)
        {
            if (this.modifiers.Contains(modifier))
            {
                this.KeyUp(modifier);
            }
            else
            {
                this.KeyDown(modifier);
            }
        }

        private void SendKeys(IEnumerable<KeyEvent> events)
        {
            foreach (var keyEvent in events)
            {
                if (keyEvent.IsNewLine())
                {
                    CruciatusFactory.Keyboard.SendEnter();
                }
                else if (keyEvent.IsModifierRelease())
                {
                    this.ReleaseModifiers();
                }
                else if (keyEvent.IsModifier())
                {
                    this.PressOrReleaseModifier(keyEvent.GetKey());
                }
                else
                {
                    this.Type(keyEvent.GetCharacter());
                }
            }
        }

        private void Type(char key)
        {
            string str = Convert.ToString(key);

            if (this.modifiers.Contains(Keys.LeftShift) || this.modifiers.Contains(Keys.Shift))
            {
                str = str.ToUpper();
            }

            CruciatusFactory.Keyboard.SendText(str);
        }

        #endregion
    }
}
