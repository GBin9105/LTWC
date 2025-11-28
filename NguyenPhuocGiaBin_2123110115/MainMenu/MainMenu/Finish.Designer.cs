namespace MainMenu
{
    partial class FinishForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblName
            //
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(30, 30);
            this.lblName.Size = new System.Drawing.Size(320, 30);
            //
            // lblScore
            //
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblScore.Location = new System.Drawing.Point(30, 80);
            this.lblScore.Size = new System.Drawing.Size(320, 30);
            //
            // lblPercent
            //
            this.lblPercent.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPercent.Location = new System.Drawing.Point(30, 130);
            this.lblPercent.Size = new System.Drawing.Size(320, 30);
            //
            // btnRetry
            //
            this.btnRetry.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRetry.Text = "Làm lại";
            this.btnRetry.Location = new System.Drawing.Point(50, 190);
            this.btnRetry.Size = new System.Drawing.Size(120, 45);
            this.btnRetry.Click += btnRetry_Click;
            //
            // btnExit
            //
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExit.Text = "Thoát";
            this.btnExit.Location = new System.Drawing.Point(200, 190);
            this.btnExit.Size = new System.Drawing.Size(120, 45);
            this.btnExit.Click += btnExit_Click;
            //
            // FinishForm
            //
            this.ClientSize = new System.Drawing.Size(380, 260);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.btnExit);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kết Quả Bài Thi";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnExit;
    }
}
