namespace Article07
{
    partial class Article07
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.TextBox tbYear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lbYear = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Location = new System.Drawing.Point(30, 30);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(36, 16);
            this.lbYear.TabIndex = 0;
            this.lbYear.Text = "Year";
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(100, 27);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(150, 22);
            this.tbYear.TabIndex = 1;
            this.tbYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYear_KeyPress);
            this.tbYear.Validating += new System.ComponentModel.CancelEventHandler(this.tbYear_Validating);
            // 
            // Article07
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 100);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.lbYear);
            this.Name = "Article07";
            this.Text = "Article07 - TextBox";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
