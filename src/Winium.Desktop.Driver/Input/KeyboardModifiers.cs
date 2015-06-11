namespace Winium.Desktop.Driver.Input
{
    #region using

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using WindowsInput.Native;

    using OpenQA.Selenium;

    #endregion

    class KeyboardModifiers : List<string>
    {
        #region Priavte Static Fields

        private static readonly List<string> AllowedModifiers = new List<string>() { Keys.Control, Keys.LeftControl, Keys.Shift, Keys.LeftShift, Keys.Alt, Keys.LeftAlt };
        private static readonly Dictionary<string, VirtualKeyCode> ModifiersMap = new Dictionary<string, VirtualKeyCode>()
        {
            {Keys.Control, VirtualKeyCode.CONTROL},
            {Keys.Shift, VirtualKeyCode.SHIFT},
            {Keys.Alt, VirtualKeyCode.MENU},
        };

        #endregion

        #region Public Static Methods and Operators

        public static bool IsModifier(string key)
        {
            return KeyboardModifiers.AllowedModifiers.Contains(key);
        }

        public static VirtualKeyCode GetVirtualKeyCode(string key)
        {
            VirtualKeyCode virtualKey;

            if (KeyboardModifiers.ModifiersMap.TryGetValue(key, out virtualKey))
            {
                return virtualKey;
            }

            return default(VirtualKeyCode);
        }

        public static string GetKeyFromUnicode(char key)
        {
            return KeyboardModifiers.AllowedModifiers.Find(modifier => modifier[0] == key);
        }

        #endregion
    }
}
