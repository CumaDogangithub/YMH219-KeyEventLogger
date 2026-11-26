using System.Collections.Generic;
using System.Windows.Forms;

namespace YMH219_KeyEventLogger
{
    /// <summary>
    /// Keys değerlerini kullanıcının okuyabileceği tuş adlarına çevirir.
    /// </summary>
    public static class KeyNameFormatter
    {
        /// <summary>
        /// Tuşun okunabilir adını döndürür. Örneğin Keys.D5 için "5", Keys.Return için "Enter".
        /// </summary>
        public static string GetName(Keys keyCode)
        {
            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
                return ((int)(keyCode - Keys.D0)).ToString();

            if (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9)
                return "NumPad " + (int)(keyCode - Keys.NumPad0);

            switch (keyCode)
            {
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    return "Shift";
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    return "Ctrl";
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                    return "Alt";
                case Keys.LWin:
                case Keys.RWin:
                    return "Windows";
                case Keys.Return:
                    return "Enter";
                case Keys.Back:
                    return "Backspace";
                case Keys.Space:
                    return "Boşluk";
                case Keys.Escape:
                    return "Esc";
                case Keys.Capital:
                    return "Caps Lock";
                case Keys.Prior:
                    return "Page Up";
                case Keys.Next:
                    return "Page Down";
                case Keys.Snapshot:
                    return "Print Screen";
                case Keys.Up:
                    return "Yukarı Ok";
                case Keys.Down:
                    return "Aşağı Ok";
                case Keys.Left:
                    return "Sol Ok";
                case Keys.Right:
                    return "Sağ Ok";
                default:
                    return keyCode.ToString();   // A, F5, Tab, Delete, OemComma ...
            }
        }

        /// <summary>
        /// Tuşla birlikte basılı tutulan Ctrl, Shift ve Alt tuşlarını "Ctrl+Shift" biçiminde döndürür.
        /// Basılan tuşun kendisi listeye eklenmez.
        /// </summary>
        public static string GetModifiers(Keys keyCode, Keys modifiers)
        {
            var parts = new List<string>();

            if ((modifiers & Keys.Control) != 0 && keyCode != Keys.ControlKey)
                parts.Add("Ctrl");

            if ((modifiers & Keys.Shift) != 0 && keyCode != Keys.ShiftKey)
                parts.Add("Shift");

            if ((modifiers & Keys.Alt) != 0 && keyCode != Keys.Menu)
                parts.Add("Alt");

            return string.Join("+", parts);
        }
    }
}
