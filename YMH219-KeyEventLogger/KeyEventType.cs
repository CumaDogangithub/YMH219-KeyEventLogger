namespace YMH219_KeyEventLogger
{
    /// <summary>
    /// Kaydedilen klavye olayının türü. Adlar Windows Forms olaylarıyla aynıdır.
    /// </summary>
    public enum KeyEventType
    {
        /// <summary>Tuşa basıldı.</summary>
        KeyDown,

        /// <summary>Basılan tuş bir karakter üretti.</summary>
        KeyPress,

        /// <summary>Tuş bırakıldı.</summary>
        KeyUp
    }
}
