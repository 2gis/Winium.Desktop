namespace Winium.Desktop.Driver.Input
{
    #region using

    using System.Collections.Generic;

    using OpenQA.Selenium;

    using WindowsInput.Native;

    #endregion

    internal class KeyboardModifiers : List<string>
    {
        #region Static Fields

        private static readonly List<string> Modifiers = new List<string>
                                                             {
                                                                 Keys.Control, 
                                                                 Keys.LeftControl, 
                                                                 Keys.Shift, 
                                                                 Keys.LeftShift, 
                                                                 Keys.Alt, 
                                                                 Keys.LeftAlt
                                                             };

        private static readonly Dictionary<string, VirtualKeyCode> ModifiersMap =
            new Dictionary<string, VirtualKeyCode>
                {
                    { Keys.Control, VirtualKeyCode.CONTROL }, 
                    { Keys.Shift, VirtualKeyCode.SHIFT }, 
                    { Keys.Alt, VirtualKeyCode.MENU }, 
                };

        #endregion

        #region Public Methods and Operators

        public static string GetKeyFromUnicode(char key)
        {
            return Modifiers.Find(modifier => modifier[0] == key);
        }

        public static VirtualKeyCode GetVirtualKeyCode(string key)
        {
            VirtualKeyCode virtualKey;

            if (ModifiersMap.TryGetValue(key, out virtualKey))
            {
                return virtualKey;
            }

            return default(VirtualKeyCode);
        }

        public static bool IsModifier(string key)
        {
            return Modifiers.Contains(key);
        }

        #endregion
    }
}
