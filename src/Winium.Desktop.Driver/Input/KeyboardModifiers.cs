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

    class KeyboardModifiers : IList<string>
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

        #region Private Fields

        private List<string> modifiersList = new List<string>();

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

        #region Public Methods and Operators

        public void Add(string item)
        {
            this.modifiersList.Add(item);
        }

        public void Clear()
        {
            this.modifiersList.Clear(); ;
        }

        public bool Contains(string item)
        {
            return this.modifiersList.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            this.modifiersList.CopyTo(array, arrayIndex);
        }

        public bool Remove(string item)
        {
            return this.modifiersList.Remove(item);
        }

        public int Count => this.modifiersList.Count;

        public bool IsReadOnly => false;

        public int IndexOf(string item)
        {
            return this.modifiersList.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            this.modifiersList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.modifiersList.RemoveAt(index);
        }

        public string this[int index]
        {
            get
            {
                return this.modifiersList[index];
            }
            set
            {
                this.modifiersList[index] = value;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.modifiersList.GetEnumerator();
        }

        #endregion

        #region Private Methods and Operators

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
