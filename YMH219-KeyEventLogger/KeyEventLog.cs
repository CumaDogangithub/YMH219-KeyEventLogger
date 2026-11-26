using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace YMH219_KeyEventLogger
{
    /// <summary>
    /// Klavye olaylarının kaydını tutar: kayıt durumu, kayıt listesi, istatistik ve filtreleme.
    /// Arayüzden bağımsızdır; Form yalnızca bu sınıfın metotlarını çağırır.
    /// </summary>
    public class KeyEventLog
    {
        private readonly List<KeyEventRecord> records = new List<KeyEventRecord>();

        /// <summary>Her tuşa kaç kez basıldığı (istatistik için).</summary>
        private readonly Dictionary<string, int> pressCounts = new Dictionary<string, int>();

        /// <summary>Şu anda basılı tutulan tuşlar ve basıldıkları an.</summary>
        private readonly Dictionary<Keys, DateTime> heldKeys = new Dictionary<Keys, DateTime>();

        public bool IsRecording { get; private set; }

        /// <summary>KeyPress (karakter) olayları da kaydedilsin mi?</summary>
        public bool IncludeKeyPress { get; set; } = true;

        /// <summary>KeyUp (bırakma) olayları da kaydedilsin mi?</summary>
        public bool IncludeKeyUp { get; set; } = true;

        public IReadOnlyList<KeyEventRecord> Records
        {
            get { return records; }
        }

        /// <summary>Toplam tuş basışı sayısı (yalnızca KeyDown olayları sayılır).</summary>
        public int TotalKeyPresses { get; private set; }

        public void Start()
        {
            IsRecording = true;
        }

        public void Stop()
        {
            IsRecording = false;
            heldKeys.Clear();
        }

        /// <summary>
        /// Tuşa basılmasını kaydeder. Kayıt kapalıysa veya tuş zaten basılı tutuluyorsa
        /// (klavyenin otomatik tekrarı) null döner.
        /// </summary>
        public KeyEventRecord? RecordKeyDown(Keys keyCode, Keys modifiers)
        {
            if (!IsRecording || heldKeys.ContainsKey(keyCode))
                return null;

            DateTime now = DateTime.Now;
            heldKeys[keyCode] = now;

            string name = KeyNameFormatter.GetName(keyCode);
            TotalKeyPresses++;
            pressCounts.TryGetValue(name, out int count);
            pressCounts[name] = count + 1;

            return Add(new KeyEventRecord(now, KeyEventType.KeyDown, name,
                KeyNameFormatter.GetModifiers(keyCode, modifiers)));
        }

        /// <summary>
        /// Basılan tuşun ürettiği karakteri kaydeder. Enter, Backspace gibi kontrol karakterleri atlanır.
        /// </summary>
        public KeyEventRecord? RecordKeyPress(char character)
        {
            if (!IsRecording || !IncludeKeyPress || char.IsControl(character))
                return null;

            return Add(new KeyEventRecord(DateTime.Now, KeyEventType.KeyPress, "'" + character + "'", ""));
        }

        /// <summary>
        /// Tuşun bırakılmasını kaydeder ve tuşun ne kadar süre basılı kaldığını hesaplar.
        /// </summary>
        public KeyEventRecord? RecordKeyUp(Keys keyCode)
        {
            if (!IsRecording)
                return null;

            DateTime now = DateTime.Now;
            string detail = "";

            if (heldKeys.TryGetValue(keyCode, out DateTime pressedAt))
            {
                detail = (int)(now - pressedAt).TotalMilliseconds + " ms basılı kaldı";
                heldKeys.Remove(keyCode);
            }

            if (!IncludeKeyUp)
                return null;

            return Add(new KeyEventRecord(now, KeyEventType.KeyUp, KeyNameFormatter.GetName(keyCode), detail));
        }

        /// <summary>
        /// Pencere odağı kaybedince çağrılır. Odak yokken KeyUp olayı gelmediği için
        /// basılı tuş bilgisi sıfırlanır.
        /// </summary>
        public void ReleaseAllKeys()
        {
            heldKeys.Clear();
        }

        public void Clear()
        {
            records.Clear();
            pressCounts.Clear();
            heldKeys.Clear();
            TotalKeyPresses = 0;
        }

        /// <summary>En çok basılan tuşları basılma sayısına göre azalan sırada döndürür.</summary>
        public List<KeyValuePair<string, int>> GetTopKeys(int count)
        {
            return pressCounts
                .OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Verilen zaman aralığındaki kayıtları döndürür. Karşılaştırma saniye hassasiyetindedir
        /// ve iki uç da aralığa dahildir.
        /// </summary>
        public List<KeyEventRecord> GetRecordsBetween(DateTime from, DateTime to)
        {
            DateTime start = TruncateToSecond(from);
            DateTime endExclusive = TruncateToSecond(to).AddSeconds(1);

            return records
                .Where(record => record.Timestamp >= start && record.Timestamp < endExclusive)
                .ToList();
        }

        private KeyEventRecord Add(KeyEventRecord record)
        {
            records.Add(record);
            return record;
        }

        /// <summary>Zamanın milisaniye kısmını atar (saniye hassasiyetinde karşılaştırma için).</summary>
        public static DateTime TruncateToSecond(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
        }
    }
}
