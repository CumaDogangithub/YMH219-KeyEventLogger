using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YMH219_KeyEventLogger
{
    public partial class Form1 : Form
    {
        private const int TopKeyCount = 5;

        private static readonly Color RecordingColor = Color.FromArgb(46, 125, 50);   // yeşil: kayıt açık
        private static readonly Color NoFocusColor = Color.FromArgb(230, 126, 0);     // turuncu: pencere etkin değil
        private static readonly Color StoppedColor = Color.FromArgb(97, 97, 97);      // gri: kayıt kapalı

        private readonly KeyEventLog log = new KeyEventLog();

        /// <summary>Filtre uygulanmışsa ekranda gösterilen kayıtlar; filtre yoksa null.</summary>
        private List<KeyEventRecord>? filteredRecords;

        private bool isWindowActive;
        private bool hasUnsavedRecords;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateRecordingUi();
            UpdateStatistics();
            SetFilterRangeToRecords();
            SetStatus("Hazır. Tuşları kaydetmek için Başlat düğmesine basın.");
        }

        // ------------------------------------------------------------------
        // Klavye olayları
        // Form.KeyPreview = true olduğu için olaylar önce forma gelir.
        // Bu olaylar yalnızca bu pencere etkinken (odaktayken) tetiklenir.
        // ------------------------------------------------------------------

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!log.IsRecording)
                return;

            KeyEventRecord? record = log.RecordKeyDown(e.KeyCode, e.Modifiers);
            if (record != null)
            {
                ShowRecord(record);
                UpdateStatistics();
            }

            // Kayıt açıkken tuş, odaktaki düğmeye gitmesin (Boşluk veya Enter düğmeye basmasın).
            // Alt+F4 ile pencereyi kapatmak serbest bırakılır.
            bool isCloseShortcut = e.Alt && e.KeyCode == Keys.F4;
            if (!isCloseShortcut)
                e.Handled = true;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!log.IsRecording)
                return;

            KeyEventRecord? record = log.RecordKeyPress(e.KeyChar);
            if (record != null)
                ShowRecord(record);

            e.Handled = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!log.IsRecording)
                return;

            KeyEventRecord? record = log.RecordKeyUp(e.KeyCode);
            if (record != null)
                ShowRecord(record);

            e.Handled = true;
        }

        /// <summary>
        /// Tab, Enter, Esc ve ok tuşları normalde odak değiştirir veya düğmeye basar ve KeyDown
        /// olayına ulaşmaz. Kayıt açıkken bu davranış kapatılır; böylece bu tuşlar da kaydedilir.
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (log.IsRecording)
                return false;

            return base.ProcessDialogKey(keyData);
        }

        // ------------------------------------------------------------------
        // Pencere odağı
        // ------------------------------------------------------------------

        private void Form1_Activated(object sender, EventArgs e)
        {
            isWindowActive = true;
            UpdateRecordingUi();

            if (log.IsRecording)
                SetStatus("Kayıt sürüyor. Bu pencere etkinken basılan tuşlar listelenir.");
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            isWindowActive = false;
            log.ReleaseAllKeys();
            UpdateRecordingUi();

            if (log.IsRecording)
                SetStatus("Pencere etkin değil: başka pencerelerde basılan tuşlar yakalanmaz.");
        }

        // ------------------------------------------------------------------
        // Kayıt kontrolü
        // ------------------------------------------------------------------

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (filteredRecords != null)
                ShowAllRecords();   // yeni kayıtlar görünsün diye filtre kaldırılır

            log.Start();
            UpdateRecordingUi();
            SetStatus("Kayıt başladı. Bu pencere etkinken basılan tuşlar listelenir.");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            log.Stop();
            UpdateRecordingUi();
            SetFilterRangeToRecords();
            SetStatus("Kayıt durduruldu. Toplam kayıt: " + log.Records.Count);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (log.Records.Count == 0)
            {
                SetStatus("Temizlenecek kayıt yok.");
                return;
            }

            if (hasUnsavedRecords)
            {
                DialogResult answer = MessageBox.Show(this,
                    "Kaydedilmemiş kayıtlar silinecek. Devam edilsin mi?", "Temizle",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer != DialogResult.Yes)
                    return;
            }

            log.Clear();
            filteredRecords = null;
            hasUnsavedRecords = false;
            lstEvents.Items.Clear();
            UpdateStatistics();
            SetFilterRangeToRecords();
            SetStatus("Kayıtlar temizlendi.");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Filtre varsa yalnızca ekranda görünen kayıtlar kaydedilir.
            IReadOnlyList<KeyEventRecord> recordsToSave = filteredRecords ?? log.Records;

            if (recordsToSave.Count == 0)
            {
                ShowWarning("Kaydedilecek kayıt yok.");
                return;
            }

            saveFileDialog.FileName = "tus_kayitlari_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                LogFileWriter.Save(saveFileDialog.FileName, recordsToSave);

                if (filteredRecords == null)
                    hasUnsavedRecords = false;

                SetStatus(recordsToSave.Count + " kayıt kaydedildi: " + Path.GetFileName(saveFileDialog.FileName));
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                SetStatus("Dosya kaydedilemedi.");
                ShowError("Dosya kaydedilemedi." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private void chkKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            log.IncludeKeyPress = chkKeyPress.Checked;
        }

        private void chkKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            log.IncludeKeyUp = chkKeyUp.Checked;
        }

        // ------------------------------------------------------------------
        // Zaman aralığı filtresi
        // ------------------------------------------------------------------

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                ShowWarning("Başlangıç zamanı bitiş zamanından sonra olamaz.");
                return;
            }

            filteredRecords = log.GetRecordsBetween(dtpFrom.Value, dtpTo.Value);
            ShowRecords(filteredRecords);
            SetStatus("Filtre uygulandı: " + filteredRecords.Count + " / " + log.Records.Count + " kayıt gösteriliyor.");
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAllRecords();
            SetStatus("Tüm kayıtlar gösteriliyor: " + log.Records.Count);
        }

        private void ShowAllRecords()
        {
            filteredRecords = null;
            ShowRecords(log.Records);
        }

        /// <summary>Filtre kutularını ilk ve son kaydın zamanına ayarlar.</summary>
        private void SetFilterRangeToRecords()
        {
            DateTime first = DateTime.Now;
            DateTime last = DateTime.Now;

            if (log.Records.Count > 0)
            {
                first = log.Records[0].Timestamp;
                last = log.Records[log.Records.Count - 1].Timestamp;
            }

            // Filtre saniye hassasiyetinde çalıştığı için milisaniyeler atılır.
            dtpFrom.Value = KeyEventLog.TruncateToSecond(first);
            dtpTo.Value = KeyEventLog.TruncateToSecond(last);
        }

        // ------------------------------------------------------------------
        // Arayüz güncelleme
        // ------------------------------------------------------------------

        /// <summary>Yeni kaydı listenin sonuna ekler ve listeyi son satıra kaydırır.</summary>
        private void ShowRecord(KeyEventRecord record)
        {
            lstEvents.Items.Add(record.ToString());
            lstEvents.TopIndex = lstEvents.Items.Count - 1;
            hasUnsavedRecords = true;
        }

        private void ShowRecords(IReadOnlyList<KeyEventRecord> records)
        {
            lstEvents.BeginUpdate();
            lstEvents.Items.Clear();

            foreach (KeyEventRecord record in records)
                lstEvents.Items.Add(record.ToString());

            lstEvents.EndUpdate();

            if (lstEvents.Items.Count > 0)
                lstEvents.TopIndex = lstEvents.Items.Count - 1;
        }

        /// <summary>Kayıt durumu göstergesini ve düğmelerin etkinliğini günceller.</summary>
        private void UpdateRecordingUi()
        {
            btnStart.Enabled = !log.IsRecording;
            btnStop.Enabled = log.IsRecording;
            grpFilter.Enabled = !log.IsRecording;   // filtre yalnızca kayıt durmuşken kullanılır

            if (!log.IsRecording)
            {
                lblState.Text = "KAYIT KAPALI";
                lblState.BackColor = StoppedColor;
            }
            else if (isWindowActive)
            {
                lblState.Text = "KAYIT AÇIK";
                lblState.BackColor = RecordingColor;
            }
            else
            {
                lblState.Text = "KAYIT AÇIK - ODAK YOK";
                lblState.BackColor = NoFocusColor;
            }
        }

        private void UpdateStatistics()
        {
            lblTotal.Text = "Toplam tuş basışı: " + log.TotalKeyPresses;

            List<KeyValuePair<string, int>> topKeys = log.GetTopKeys(TopKeyCount);
            if (topKeys.Count == 0)
            {
                lblTopKeys.Text = "Henüz kayıt yok.";
                return;
            }

            var lines = new List<string>();
            for (int i = 0; i < topKeys.Count; i++)
                lines.Add((i + 1) + ". " + topKeys[i].Key + " - " + topKeys[i].Value + " kez");

            lblTopKeys.Text = string.Join(Environment.NewLine, lines);
        }

        private void SetStatus(string message)
        {
            lblStatus.Text = message;
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(this, message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(this, message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
