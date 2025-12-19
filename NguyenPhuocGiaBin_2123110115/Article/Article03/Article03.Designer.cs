namespace Article03
{
    partial class Article03
    {
        private System.ComponentModel.IContainer components = null;

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
            this.SuspendLayout();
            // 
            // Article03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Article03";
            this.Text = "Article03";
            this.Load += new System.EventHandler(this.Article03_Load);
            this.ResizeEnd += new System.EventHandler(this.Article03_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.Article03_LocationChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Article03_FormClosing);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
