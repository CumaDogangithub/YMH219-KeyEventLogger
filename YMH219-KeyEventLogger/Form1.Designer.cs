namespace YMH219_KeyEventLogger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstEvents = new ListBox();
            lblState = new Label();
            grpControls = new GroupBox();
            chkKeyUp = new CheckBox();
            chkKeyPress = new CheckBox();
            btnSave = new Button();
            btnClear = new Button();
            btnStop = new Button();
            btnStart = new Button();
            grpStats = new GroupBox();
            lblTopKeys = new Label();
            lblTopCaption = new Label();
            lblTotal = new Label();
            grpFilter = new GroupBox();
            btnShowAll = new Button();
            btnFilter = new Button();
            dtpTo = new DateTimePicker();
            lblTo = new Label();
            dtpFrom = new DateTimePicker();
            lblFrom = new Label();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            saveFileDialog = new SaveFileDialog();
            grpControls.SuspendLayout();
            grpStats.SuspendLayout();
            grpFilter.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // lstEvents
            //
            lstEvents.Font = new Font("Consolas", 9F);
            lstEvents.FormattingEnabled = true;
            lstEvents.HorizontalScrollbar = true;
            lstEvents.IntegralHeight = false;
            lstEvents.ItemHeight = 14;
            lstEvents.Location = new Point(12, 12);
            lstEvents.Name = "lstEvents";
            lstEvents.Size = new Size(560, 470);
            lstEvents.TabIndex = 0;
            //
            // lblState
            //
            lblState.BackColor = Color.Gray;
            lblState.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblState.ForeColor = Color.White;
            lblState.Location = new Point(584, 12);
            lblState.Name = "lblState";
            lblState.Size = new Size(304, 44);
            lblState.TabIndex = 1;
            lblState.Text = "KAYIT KAPALI";
            lblState.TextAlign = ContentAlignment.MiddleCenter;
            //
            // grpControls
            //
            grpControls.Controls.Add(chkKeyUp);
            grpControls.Controls.Add(chkKeyPress);
            grpControls.Controls.Add(btnSave);
            grpControls.Controls.Add(btnClear);
            grpControls.Controls.Add(btnStop);
            grpControls.Controls.Add(btnStart);
            grpControls.Location = new Point(584, 62);
            grpControls.Name = "grpControls";
            grpControls.Size = new Size(304, 124);
            grpControls.TabIndex = 2;
            grpControls.TabStop = false;
            grpControls.Text = "Kayıt Kontrolü";
            //
            // chkKeyUp
            //
            chkKeyUp.AutoSize = true;
            chkKeyUp.Checked = true;
            chkKeyUp.CheckState = CheckState.Checked;
            chkKeyUp.Location = new Point(156, 96);
            chkKeyUp.Name = "chkKeyUp";
            chkKeyUp.Size = new Size(100, 19);
            chkKeyUp.TabIndex = 5;
            chkKeyUp.Text = "KeyUp olayları";
            chkKeyUp.UseVisualStyleBackColor = true;
            chkKeyUp.CheckedChanged += chkKeyUp_CheckedChanged;
            //
            // chkKeyPress
            //
            chkKeyPress.AutoSize = true;
            chkKeyPress.Checked = true;
            chkKeyPress.CheckState = CheckState.Checked;
            chkKeyPress.Location = new Point(12, 96);
            chkKeyPress.Name = "chkKeyPress";
            chkKeyPress.Size = new Size(113, 19);
            chkKeyPress.TabIndex = 4;
            chkKeyPress.Text = "KeyPress olayları";
            chkKeyPress.UseVisualStyleBackColor = true;
            chkKeyPress.CheckedChanged += chkKeyPress_CheckedChanged;
            //
            // btnSave
            //
            btnSave.Location = new Point(156, 58);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(136, 28);
            btnSave.TabIndex = 3;
            btnSave.Text = "Kaydet (.txt)";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            //
            // btnClear
            //
            btnClear.Location = new Point(12, 58);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(136, 28);
            btnClear.TabIndex = 2;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            //
            // btnStop
            //
            btnStop.Location = new Point(156, 24);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(136, 28);
            btnStop.TabIndex = 1;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            //
            // btnStart
            //
            btnStart.Location = new Point(12, 24);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(136, 28);
            btnStart.TabIndex = 0;
            btnStart.Text = "Başlat";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            //
            // grpStats
            //
            grpStats.Controls.Add(lblTopKeys);
            grpStats.Controls.Add(lblTopCaption);
            grpStats.Controls.Add(lblTotal);
            grpStats.Location = new Point(584, 192);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(304, 150);
            grpStats.TabIndex = 3;
            grpStats.TabStop = false;
            grpStats.Text = "İstatistik";
            //
            // lblTopKeys
            //
            lblTopKeys.Location = new Point(12, 66);
            lblTopKeys.Name = "lblTopKeys";
            lblTopKeys.Size = new Size(280, 78);
            lblTopKeys.TabIndex = 2;
            lblTopKeys.Text = "Henüz kayıt yok.";
            //
            // lblTopCaption
            //
            lblTopCaption.AutoSize = true;
            lblTopCaption.Location = new Point(12, 46);
            lblTopCaption.Name = "lblTopCaption";
            lblTopCaption.Size = new Size(133, 15);
            lblTopCaption.TabIndex = 1;
            lblTopCaption.Text = "En sık kullanılan tuşlar:";
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(12, 24);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(113, 15);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Toplam tuş basışı: 0";
            //
            // grpFilter
            //
            grpFilter.Controls.Add(btnShowAll);
            grpFilter.Controls.Add(btnFilter);
            grpFilter.Controls.Add(dtpTo);
            grpFilter.Controls.Add(lblTo);
            grpFilter.Controls.Add(dtpFrom);
            grpFilter.Controls.Add(lblFrom);
            grpFilter.Location = new Point(584, 348);
            grpFilter.Name = "grpFilter";
            grpFilter.Size = new Size(304, 134);
            grpFilter.TabIndex = 4;
            grpFilter.TabStop = false;
            grpFilter.Text = "Zaman Aralığı Filtresi";
            //
            // btnShowAll
            //
            btnShowAll.Location = new Point(156, 92);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(136, 28);
            btnShowAll.TabIndex = 5;
            btnShowAll.Text = "Tümünü Göster";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            //
            // btnFilter
            //
            btnFilter.Location = new Point(12, 92);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(136, 28);
            btnFilter.TabIndex = 4;
            btnFilter.Text = "Filtrele";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            //
            // dtpTo
            //
            dtpTo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(84, 54);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(208, 23);
            dtpTo.TabIndex = 3;
            //
            // lblTo
            //
            lblTo.AutoSize = true;
            lblTo.Location = new Point(12, 58);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(33, 15);
            lblTo.TabIndex = 2;
            lblTo.Text = "Bitiş:";
            //
            // dtpFrom
            //
            dtpFrom.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(84, 24);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(208, 23);
            dtpFrom.TabIndex = 1;
            //
            // lblFrom
            //
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(12, 28);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(61, 15);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "Başlangıç:";
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 494);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(900, 22);
            statusStrip.SizingGrip = false;
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip";
            //
            // lblStatus
            //
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(34, 17);
            lblStatus.Text = "Hazır";
            //
            // saveFileDialog
            //
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Metin dosyası (*.txt)|*.txt";
            saveFileDialog.Title = "Kayıtları Kaydet";
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 516);
            Controls.Add(statusStrip);
            Controls.Add(grpFilter);
            Controls.Add(grpStats);
            Controls.Add(grpControls);
            Controls.Add(lblState);
            Controls.Add(lstEvents);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YMH219 - Key Event Logger";
            Activated += Form1_Activated;
            Deactivate += Form1_Deactivate;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyPress += Form1_KeyPress;
            KeyUp += Form1_KeyUp;
            grpControls.ResumeLayout(false);
            grpControls.PerformLayout();
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            grpFilter.ResumeLayout(false);
            grpFilter.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstEvents;
        private Label lblState;
        private GroupBox grpControls;
        private CheckBox chkKeyUp;
        private CheckBox chkKeyPress;
        private Button btnSave;
        private Button btnClear;
        private Button btnStop;
        private Button btnStart;
        private GroupBox grpStats;
        private Label lblTopKeys;
        private Label lblTopCaption;
        private Label lblTotal;
        private GroupBox grpFilter;
        private Button btnShowAll;
        private Button btnFilter;
        private DateTimePicker dtpTo;
        private Label lblTo;
        private DateTimePicker dtpFrom;
        private Label lblFrom;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private SaveFileDialog saveFileDialog;
    }
}
