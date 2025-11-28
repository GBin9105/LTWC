namespace MainMenu
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(50, 30);
            this.lblTitle.Size = new System.Drawing.Size(300, 40);
            this.lblTitle.Text = "Nhập Tên Người Thi";
            //
            // txtName
            //
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtName.Location = new System.Drawing.Point(50, 100);
            this.txtName.Size = new System.Drawing.Size(300, 32);
            //
            // btnStart
            //
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnStart.Text = "Bắt đầu thi";
            this.btnStart.Location = new System.Drawing.Point(100, 160);
            this.btnStart.Size = new System.Drawing.Size(200, 45);
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // LoginForm
            //
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnStart);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnStart;
    }
}
