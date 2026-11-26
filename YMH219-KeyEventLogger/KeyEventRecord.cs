using System;
using System.Globalization;

namespace YMH219_KeyEventLogger
{
    /// <summary>
    /// Tek bir klavye olayı kaydı: zaman, olay türü, tuş adı ve ek bilgi.
    /// </summary>
    public class KeyEventRecord
    {
        public DateTime Timestamp { get; }
        public KeyEventType EventType { get; }

        /// <summary>Tuşun adı (örneğin "A", "Enter") veya KeyPress için üretilen karakter.</summary>
        public string KeyName { get; }

        /// <summary>Ek bilgi: birlikte basılan tuşlar veya tuşun basılı kalma süresi.</summary>
        public string Detail { get; }

        public KeyEventRecord(DateTime timestamp, KeyEventType eventType, string keyName, string detail)
        {
            Timestamp = timestamp;
            EventType = eventType;
            KeyName = keyName;
            Detail = detail;
        }

        /// <summary>
        /// Kaydı hem ekranda hem dosyada kullanılan tek satırlık biçime çevirir:
        /// "26.11.2026 10:50:12.345 | KeyDown  | A            | Shift"
        /// </summary>
        public override string ToString()
        {
            string time = Timestamp.ToString("dd.MM.yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

            if (Detail.Length == 0)
                return string.Format("{0} | {1,-8} | {2}", time, EventType, KeyName);

            return string.Format("{0} | {1,-8} | {2,-12} | {3}", time, EventType, KeyName, Detail);
        }
    }
}
