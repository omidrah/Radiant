namespace WinFormsApp1
{
    partial class UrlConfigForm
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
            txtUrlStreaming = new TextBox();
            btnReset = new Button();
            btnSetSave = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 67);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Url Streaming";
            // 
            // txtUrlStreaming
            // 
            txtUrlStreaming.Location = new Point(129, 64);
            txtUrlStreaming.Name = "txtUrlStreaming";
            txtUrlStreaming.Size = new Size(558, 23);
            txtUrlStreaming.TabIndex = 2;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(129, 131);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(105, 23);
            btnReset.TabIndex = 7;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnSetSave
            // 
            btnSetSave.Location = new Point(582, 131);
            btnSetSave.Name = "btnSetSave";
            btnSetSave.Size = new Size(105, 23);
            btnSetSave.TabIndex = 6;
            btnSetSave.Text = "Set And Save";
            btnSetSave.UseVisualStyleBackColor = true;
            btnSetSave.Click += btnSetSave_Click;
            // 
            // UrlConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 178);
            Controls.Add(btnReset);
            Controls.Add(btnSetSave);
            Controls.Add(txtUrlStreaming);
            Controls.Add(label1);
            Name = "UrlConfigForm";
            Text = "UrlConfigForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUrlStreaming;
        private Button btnReset;
        private Button btnSetSave;
    }
}