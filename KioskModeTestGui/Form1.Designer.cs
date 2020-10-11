namespace KioskModeTestGui {
    partial class Form1 {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnStartKiosk = new System.Windows.Forms.Button();
            this.BtnEndKiosk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnStartKiosk
            // 
            this.BtnStartKiosk.Location = new System.Drawing.Point(21, 12);
            this.BtnStartKiosk.Name = "BtnStartKiosk";
            this.BtnStartKiosk.Size = new System.Drawing.Size(75, 23);
            this.BtnStartKiosk.TabIndex = 0;
            this.BtnStartKiosk.Text = "Start Kiosk";
            this.BtnStartKiosk.UseVisualStyleBackColor = true;
            this.BtnStartKiosk.Click += new System.EventHandler(this.BtnStartKiosk_Click);
            // 
            // BtnEndKiosk
            // 
            this.BtnEndKiosk.Location = new System.Drawing.Point(155, 12);
            this.BtnEndKiosk.Name = "BtnEndKiosk";
            this.BtnEndKiosk.Size = new System.Drawing.Size(75, 23);
            this.BtnEndKiosk.TabIndex = 1;
            this.BtnEndKiosk.Text = "End Kiosk";
            this.BtnEndKiosk.UseVisualStyleBackColor = true;
            this.BtnEndKiosk.Click += new System.EventHandler(this.BtnEndKiosk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Key- Sequence to escape from KioskMode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ESCFROMKIOSKF1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 83);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnEndKiosk);
            this.Controls.Add(this.BtnStartKiosk);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnStartKiosk;
        private System.Windows.Forms.Button BtnEndKiosk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

