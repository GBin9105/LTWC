namespace Article01
{
    partial class Article01
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
            // Article01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Article01";
            this.Text = "Article01";
            this.Load += new System.EventHandler(this.Article01_Load);
            this.ResizeEnd += new System.EventHandler(this.Article01_ResizeEnd);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
