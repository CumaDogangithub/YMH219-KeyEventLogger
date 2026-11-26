using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YMH219_KeyEventLogger
{
    /// <summary>
    /// Kayıtları yerel bir metin dosyasına yazar.
    /// </summary>
    public static class LogFileWriter
    {
        /// <summary>
        /// Kayıtları başlık bilgisiyle birlikte .txt dosyasına yazar.
        /// Dosya yazılamazsa IOException veya UnauthorizedAccessException fırlatır.
        /// </summary>
        public static void Save(string path, IReadOnlyList<KeyEventRecord> records)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine("YMH219 Key Event Logger - Kayıt Dosyası");
                writer.WriteLine("Oluşturulma: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                writer.WriteLine("Kayıt sayısı: " + records.Count);
                writer.WriteLine("Biçim: Tarih Saat | Olay | Tuş | Ek bilgi");
                writer.WriteLine(new string('-', 70));

                foreach (KeyEventRecord record in records)
                    writer.WriteLine(record.ToString());
            }
        }
    }
}
