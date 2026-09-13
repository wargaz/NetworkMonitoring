
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
            ListConnections = new ListBox();
            LoadProgress = new ProgressBar();
            LabelLoad = new Label();
            panel1.SuspendLayout();
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
            LabelResultIP.Location = new Point(18, 248);
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
            ButtonIP.Location = new Point(448, 16);
            ButtonIP.Name = "ButtonIP";
            ButtonIP.Size = new Size(132, 43);
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
            panel1.Size = new Size(599, 150);
            panel1.TabIndex = 10;
            // 
            // ListConnections
            // 
            ListConnections.FormattingEnabled = true;
            ListConnections.Location = new Point(646, 77);
            ListConnections.Name = "ListConnections";
            ListConnections.Size = new Size(882, 574);
            ListConnections.TabIndex = 11;
            // 
            // LoadProgress
            // 
            LoadProgress.Location = new Point(12, 465);
            LoadProgress.Name = "LoadProgress";
            LoadProgress.Size = new Size(587, 29);
            LoadProgress.Step = 1;
            LoadProgress.TabIndex = 12;
            // 
            // LabelLoad
            // 
            LabelLoad.AutoSize = true;
            LabelLoad.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelLoad.Location = new Point(18, 338);
            LabelLoad.Name = "LabelLoad";
            LabelLoad.Size = new Size(157, 38);
            LabelLoad.TabIndex = 13;
            LabelLoad.Text = "Laddar-text";
            // 
            // NetworkUI
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1782, 745);
            Controls.Add(LabelLoad);
            Controls.Add(LoadProgress);
            Controls.Add(ListConnections);
            Controls.Add(panel1);
            Controls.Add(LabelResultIP);
            Controls.Add(ipLabel);
            Controls.Add(labelTitle);
            Margin = new Padding(2);
            Name = "NetworkUI";
            Text = "Nätverksövervakning";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private ListBox ListConnections;
        private ProgressBar LoadProgress;
        private Label LabelLoad;
    }
}