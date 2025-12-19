namespace Article02
{
    partial class Article02
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
            // Article02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Article02";
            this.Text = "Article02";
            this.Load += new System.EventHandler(this.Article02_Load);
            this.ResizeEnd += new System.EventHandler(this.Article02_ResizeEnd);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
