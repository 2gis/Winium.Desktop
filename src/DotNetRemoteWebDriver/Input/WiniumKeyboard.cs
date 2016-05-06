#region using

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Winium.Cruciatus;
using Winium.Cruciatus.Settings;

#endregion

namespace DotNetRemoteWebDriver.Input
{
    #region using

    

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
            modifiers.Add(keyToPress);
            CruciatusFactory.Keyboard.KeyDown(key);
        }

        public void KeyUp(string keyToRelease)
        {
            var key = KeyboardModifiers.GetVirtualKeyCode(keyToRelease);
            modifiers.Remove(keyToRelease);
            CruciatusFactory.Keyboard.KeyUp(key);
        }

        public void SendKeys(char[] keysToSend)
        {
            var builder = keysToSend.Select(key => new KeyEvent(key)).ToList();

            SendKeys(builder);
        }

        #endregion

        #region Methods

        protected void ReleaseModifiers()
        {
            var tmp = modifiers.ToList();

            foreach (var modifierKey in tmp)
            {
                KeyUp(modifierKey);
            }
        }

        private void PressOrReleaseModifier(string modifier)
        {
            if (modifiers.Contains(modifier))
            {
                KeyUp(modifier);
            }
            else
            {
                KeyDown(modifier);
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
                    ReleaseModifiers();
                }
                else if (keyEvent.IsModifier())
                {
                    PressOrReleaseModifier(keyEvent.GetKey());
                }
                else
                {
                    Type(keyEvent.GetCharacter());
                }
            }
        }

        private void Type(char key)
        {
            var str = Convert.ToString(key);

            if (modifiers.Contains(Keys.LeftShift) || modifiers.Contains(Keys.Shift))
            {
                str = str.ToUpper();
            }

            CruciatusFactory.Keyboard.SendText(str);
        }

        #endregion
    }
}