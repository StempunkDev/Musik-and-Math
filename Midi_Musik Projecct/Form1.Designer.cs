namespace Midi_Musik_Projecct
{
    partial class Form1
    {
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
            if (disposing && (components != null))
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
            this.Btn_openData = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // Btn_openData
            // 
            this.Btn_openData.Location = new System.Drawing.Point(986, 420);
            this.Btn_openData.Name = "Btn_openData";
            this.Btn_openData.Size = new System.Drawing.Size(110, 23);
            this.Btn_openData.TabIndex = 0;
            this.Btn_openData.Text = "Datei Öffnen";
            this.Btn_openData.UseVisualStyleBackColor = true;
            this.Btn_openData.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 455);
            this.Controls.Add(this.Btn_openData);
            this.Name = "Form1";
            this.Text = "Midi Reader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_openData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

