
namespace Nätverksövervakning.UI
{
    partial class NetworkUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            LabelInputIP = new RichTextBox();
            labelTitle = new Label();
            ipLabel = new Label();
            LabelResultIP = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ButtonIP = new Button();
            LabelErrorIP = new Label();
            panel1 = new Panel();
            LoadProgress = new ProgressBar();
            GroupResult = new GroupBox();
            label4 = new Label();
            DataGrid = new DataGridView();
            GroupLoading = new GroupBox();
            LatencyTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            GroupResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid).BeginInit();
            GroupLoading.SuspendLayout();
            SuspendLayout();
            // 
            // LabelInputIP
            // 
            LabelInputIP.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LabelInputIP.Location = new Point(330, 16);
            LabelInputIP.Name = "LabelInputIP";
            LabelInputIP.RightToLeft = RightToLeft.No;
            LabelInputIP.Size = new Size(58, 43);
            LabelInputIP.TabIndex = 6;
            LabelInputIP.Text = "0";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Ubuntu", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(12, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(408, 47);
            labelTitle.TabIndex = 1;
            labelTitle.Text = "Nätverksövervakning";
            // 
            // ipLabel
            // 
            ipLabel.AutoSize = true;
            ipLabel.Location = new Point(12, 77);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(0, 31);
            ipLabel.TabIndex = 2;
            // 
            // LabelResultIP
            // 
            LabelResultIP.AutoSize = true;
            LabelResultIP.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelResultIP.Location = new Point(426, 26);
            LabelResultIP.Name = "LabelResultIP";
            LabelResultIP.Size = new Size(226, 41);
            LabelResultIP.TabIndex = 3;
            LabelResultIP.Text = "Min IP (resultat)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(18, 19);
            label1.Name = "label1";
            label1.Size = new Size(191, 38);
            label1.TabIndex = 4;
            label1.Text = "Ange subnet:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(215, 19);
            label2.Name = "label2";
            label2.Size = new Size(119, 38);
            label2.TabIndex = 5;
            label2.Text = "192.168.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(394, 19);
            label3.Name = "label3";
            label3.Size = new Size(36, 38);
            label3.TabIndex = 7;
            label3.Text = ".x";
            // 
            // ButtonIP
            // 
            ButtonIP.Location = new Point(459, 16);
            ButtonIP.Name = "ButtonIP";
            ButtonIP.Size = new Size(408, 43);
            ButtonIP.TabIndex = 8;
            ButtonIP.Text = "Kör";
            ButtonIP.UseVisualStyleBackColor = true;
            ButtonIP.Click += ButtonIP_Click;
            // 
            // LabelErrorIP
            // 
            LabelErrorIP.AutoSize = true;
            LabelErrorIP.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelErrorIP.ForeColor = Color.DarkRed;
            LabelErrorIP.Location = new Point(18, 67);
            LabelErrorIP.Name = "LabelErrorIP";
            LabelErrorIP.Size = new Size(99, 38);
            LabelErrorIP.TabIndex = 9;
            LabelErrorIP.Text = "Feltext";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ButtonIP);
            panel1.Controls.Add(LabelErrorIP);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(LabelInputIP);
            panel1.Location = new Point(18, 77);
            panel1.Name = "panel1";
            panel1.Size = new Size(885, 131);
            panel1.TabIndex = 10;
            // 
            // LoadProgress
            // 
            LoadProgress.Location = new Point(19, 59);
            LoadProgress.Name = "LoadProgress";
            LoadProgress.Size = new Size(786, 29);
            LoadProgress.Step = 1;
            LoadProgress.TabIndex = 12;
            // 
            // GroupResult
            // 
            GroupResult.Controls.Add(DataGrid);
            GroupResult.FlatStyle = FlatStyle.System;
            GroupResult.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GroupResult.Location = new Point(12, 225);
            GroupResult.Name = "GroupResult";
            GroupResult.Size = new Size(1741, 767);
            GroupResult.TabIndex = 15;
            GroupResult.TabStop = false;
            GroupResult.Text = "Resultat";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1596, 9);
            label4.Name = "label4";
            label4.Size = new Size(157, 31);
            label4.TabIndex = 18;
            label4.Text = "John Axelsson";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // DataGrid
            // 
            DataGrid.AllowUserToAddRows = false;
            DataGrid.AllowUserToDeleteRows = false;
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataGrid.BackgroundColor = SystemColors.HighlightText;
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid.Location = new Point(12, 58);
            DataGrid.Name = "DataGrid";
            DataGrid.ReadOnly = true;
            DataGrid.RowHeadersWidth = 51;
            DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGrid.Size = new Size(1706, 684);
            DataGrid.TabIndex = 17;
            // 
            // GroupLoading
            // 
            GroupLoading.Controls.Add(LoadProgress);
            GroupLoading.Location = new Point(914, 81);
            GroupLoading.Name = "GroupLoading";
            GroupLoading.Size = new Size(811, 127);
            GroupLoading.TabIndex = 16;
            GroupLoading.TabStop = false;
            GroupLoading.Text = "Laddar";
            // 
            // LatencyTimer
            // 
            LatencyTimer.Interval = 1000;
            LatencyTimer.Tick += LatencyTimer_Tick;
            // 
            // NetworkUI
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1765, 1004);
            Controls.Add(label4);
            Controls.Add(GroupResult);
            Controls.Add(GroupLoading);
            Controls.Add(panel1);
            Controls.Add(LabelResultIP);
            Controls.Add(ipLabel);
            Controls.Add(labelTitle);
            Margin = new Padding(2);
            Name = "NetworkUI";
            Text = "Nätverksövervakning";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            GroupResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGrid).EndInit();
            GroupLoading.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelTitle;
        private Label ipLabel;
        public Label LabelResultIP;
        private Label label1;
        private Label label2;
        private RichTextBox LabelInputIP;
        private Label label3;
        private Button ButtonIP;
        private Label LabelErrorIP;
        private Panel panel1;
        private ProgressBar LoadProgress;
        private GroupBox GroupResult;
        private GroupBox GroupLoading;
        private RichTextBox richTextBox1;
        private DataGridView DataGrid;
        private System.Windows.Forms.Timer LatencyTimer;
        private Label label4;
    }
}