namespace WinFormsApp1
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
            btn_play = new Button();
            btn_pause = new Button();
            btn_stop = new Button();
            statusLabel = new Label();
            openFileDialog1 = new OpenFileDialog();
            btn_upload = new Button();
            btnNetworkConfigFrm = new Button();
            btnPlayByUrl = new Button();
            SuspendLayout();
            // 
            // btn_play
            // 
            btn_play.Location = new Point(26, 80);
            btn_play.Name = "btn_play";
            btn_play.Size = new Size(75, 23);
            btn_play.TabIndex = 0;
            btn_play.Text = "Play";
            btn_play.UseVisualStyleBackColor = true;
            btn_play.Click += btn_play_Click;
            // 
            // btn_pause
            // 
            btn_pause.Location = new Point(142, 80);
            btn_pause.Name = "btn_pause";
            btn_pause.Size = new Size(75, 23);
            btn_pause.TabIndex = 1;
            btn_pause.Text = "Pause";
            btn_pause.UseVisualStyleBackColor = true;
            btn_pause.Click += btn_pause_Click;
            // 
            // btn_stop
            // 
            btn_stop.Location = new Point(249, 80);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(75, 23);
            btn_stop.TabIndex = 2;
            btn_stop.Text = "Stop";
            btn_stop.UseVisualStyleBackColor = true;
            btn_stop.Click += btn_stop_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(126, 124);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            statusLabel.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_upload
            // 
            btn_upload.Location = new Point(26, 27);
            btn_upload.Name = "btn_upload";
            btn_upload.Size = new Size(116, 23);
            btn_upload.TabIndex = 4;
            btn_upload.Text = "Play Your Media File";
            btn_upload.UseVisualStyleBackColor = true;
            btn_upload.Click += btn_upload_Click;
            // 
            // btnNetworkConfigFrm
            // 
            btnNetworkConfigFrm.Location = new Point(188, 27);
            btnNetworkConfigFrm.Name = "btnNetworkConfigFrm";
            btnNetworkConfigFrm.Size = new Size(116, 23);
            btnNetworkConfigFrm.TabIndex = 5;
            btnNetworkConfigFrm.Text = "Play On Network";
            btnNetworkConfigFrm.UseVisualStyleBackColor = true;
            btnNetworkConfigFrm.Click += btnNetworkConfigFrm_Click;
            // 
            // btnPlayByUrl
            // 
            btnPlayByUrl.Location = new Point(322, 27);
            btnPlayByUrl.Name = "btnPlayByUrl";
            btnPlayByUrl.Size = new Size(116, 23);
            btnPlayByUrl.TabIndex = 6;
            btnPlayByUrl.Text = "Play On Url";
            btnPlayByUrl.UseVisualStyleBackColor = true;
            btnPlayByUrl.Click += btnPlayByUrl_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1030, 606);
            Controls.Add(btnPlayByUrl);
            Controls.Add(btnNetworkConfigFrm);
            Controls.Add(btn_upload);
            Controls.Add(statusLabel);
            Controls.Add(btn_stop);
            Controls.Add(btn_pause);
            Controls.Add(btn_play);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_play;
        private Button btn_pause;
        private Button btn_stop;
        private Label statusLabel;
        private OpenFileDialog openFileDialog1;
        private Button btn_upload;
        private Button btnNetworkConfigFrm;
        private Button btnPlayByUrl;
    }
}
