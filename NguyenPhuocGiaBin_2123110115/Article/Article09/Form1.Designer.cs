namespace Article09
{
    partial class Form1
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
            this.tbSoX = new System.Windows.Forms.TextBox();
            this.tbSoY = new System.Windows.Forms.TextBox();
            this.tbKetQua = new System.Windows.Forms.TextBox();
            this.btCong = new System.Windows.Forms.Button();
            this.btNhan = new System.Windows.Forms.Button();
            this.btLuu = new System.Windows.Forms.Button();
            this.btThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbSoX
            // 
            this.tbSoX.Location = new System.Drawing.Point(80, 20);
            this.tbSoX.Name = "tbSoX";
            this.tbSoX.Size = new System.Drawing.Size(200, 22);
            // 
            // tbSoY
            // 
            this.tbSoY.Location = new System.Drawing.Point(80, 55);
            this.tbSoY.Name = "tbSoY";
            this.tbSoY.Size = new System.Drawing.Size(200, 22);
            // 
            // tbKetQua
            // 
            this.tbKetQua.Location = new System.Drawing.Point(20, 90);
            this.tbKetQua.Multiline = true;
            this.tbKetQua.Name = "tbKetQua";
            this.tbKetQua.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbKetQua.Size = new System.Drawing.Size(360, 180);
            this.tbKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)
                ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btCong
            // 
            this.btCong.Location = new System.Drawing.Point(20, 290);
            this.btCong.Name = "btCong";
            this.btCong.Size = new System.Drawing.Size(75, 30);
            this.btCong.Text = "Cộng";
            this.btCong.Click += new System.EventHandler(this.btCong_Click);
            // 
            // btNhan
            // 
            this.btNhan.Location = new System.Drawing.Point(105, 290);
            this.btNhan.Name = "btNhan";
            this.btNhan.Size = new System.Drawing.Size(75, 30);
            this.btNhan.Text = "Nhân";
            this.btNhan.Click += new System.EventHandler(this.btNhan_Click);
            // 
            // btLuu
            // 
            this.btLuu.Location = new System.Drawing.Point(190, 290);
            this.btLuu.Name = "btLuu";
            this.btLuu.Size = new System.Drawing.Size(75, 30);
            this.btLuu.Text = "Lưu";
            this.btLuu.Click += new System.EventHandler(this.btLuu_Click);
            // 
            // btThoat
            // 
            this.btThoat.Location = new System.Drawing.Point(275, 290);
            this.btThoat.Name = "btThoat";
            this.btThoat.Size = new System.Drawing.Size(75, 30);
            this.btThoat.Text = "Thoát";
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(400, 340);
            this.Controls.Add(this.tbSoX);
            this.Controls.Add(this.tbSoY);
            this.Controls.Add(this.tbKetQua);
            this.Controls.Add(this.btCong);
            this.Controls.Add(this.btNhan);
            this.Controls.Add(this.btLuu);
            this.Controls.Add(this.btThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbSoX;
        private System.Windows.Forms.TextBox tbSoY;
        private System.Windows.Forms.TextBox tbKetQua;
        private System.Windows.Forms.Button btCong;
        private System.Windows.Forms.Button btNhan;
        private System.Windows.Forms.Button btLuu;
        private System.Windows.Forms.Button btThoat;
    }
}
