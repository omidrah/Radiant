namespace WinFormsApp1
{
    partial class frmNetwork
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
            label1 = new Label();
            txtIp = new TextBox();
            txtPort = new TextBox();
            label2 = new Label();
            btnSetSave = new Button();
            btnReset = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 26);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 0;
            label1.Text = "Ip";
            // 
            // txtIp
            // 
            txtIp.Location = new Point(99, 23);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(206, 23);
            txtIp.TabIndex = 1;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(99, 66);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(206, 23);
            txtPort.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 69);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // btnSetSave
            // 
            btnSetSave.Location = new Point(422, 68);
            btnSetSave.Name = "btnSetSave";
            btnSetSave.Size = new Size(105, 23);
            btnSetSave.TabIndex = 4;
            btnSetSave.Text = "Set And Save";
            btnSetSave.UseVisualStyleBackColor = true;
            btnSetSave.Click += btnSetSave_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(422, 39);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(105, 23);
            btnReset.TabIndex = 5;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // frmNetwork
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(576, 147);
            Controls.Add(btnReset);
            Controls.Add(btnSetSave);
            Controls.Add(txtPort);
            Controls.Add(label2);
            Controls.Add(txtIp);
            Controls.Add(label1);
            Name = "frmNetwork";
            Text = "Network Config";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIp;
        private TextBox txtPort;
        private Label label2;
        private Button btnSetSave;
        private Button btnReset;
    }
}