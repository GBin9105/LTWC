namespace Article06
{
    partial class Article06
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button bt_OK;

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
            this.bt_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_OK
            // 
            this.bt_OK.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.bt_OK.Location = new System.Drawing.Point(200, 220);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(80, 30);
            this.bt_OK.TabIndex = 0;
            this.bt_OK.Text = "OK";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // Article06
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.bt_OK);
            this.Name = "Article06";
            this.Text = "Article06";
            this.ResumeLayout(false);
        }
        #endregion
    }
}
