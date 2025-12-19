namespace Article08
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tbSoX = new System.Windows.Forms.TextBox();
            this.tbSoY = new System.Windows.Forms.TextBox();
            this.tbKetQua = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btCong = new System.Windows.Forms.Button();
            this.btNhan = new System.Windows.Forms.Button();
            this.btThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbSoX
            // 
            this.tbSoX.Location = new System.Drawing.Point(90, 20);
            this.tbSoX.Name = "tbSoX";
            this.tbSoX.Size = new System.Drawing.Size(150, 22);
            // 
            // tbSoY
            // 
            this.tbSoY.Location = new System.Drawing.Point(90, 55);
            this.tbSoY.Name = "tbSoY";
            this.tbSoY.Size = new System.Drawing.Size(150, 22);
            // 
            // tbKetQua
            // 
            this.tbKetQua.Location = new System.Drawing.Point(90, 90);
            this.tbKetQua.Name = "tbKetQua";
            this.tbKetQua.ReadOnly = true;
            this.tbKetQua.Size = new System.Drawing.Size(150, 22);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Text = "Số x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Text = "Số y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 93);
            this.label3.Text = "Kết quả";
            // 
            // btCong
            // 
            this.btCong.Location = new System.Drawing.Point(20, 130);
            this.btCong.Name = "btCong";
            this.btCong.Size = new System.Drawing.Size(70, 30);
            this.btCong.Text = "Cộng";
            this.btCong.Click += new System.EventHandler(this.btCong_Click);
            // 
            // btNhan
            // 
            this.btNhan.Location = new System.Drawing.Point(100, 130);
            this.btNhan.Name = "btNhan";
            this.btNhan.Size = new System.Drawing.Size(70, 30);
            this.btNhan.Text = "Nhân";
            this.btNhan.Click += new System.EventHandler(this.btNhan_Click);
            // 
            // btThoat
            // 
            this.btThoat.Location = new System.Drawing.Point(180, 130);
            this.btThoat.Name = "btThoat";
            this.btThoat.Size = new System.Drawing.Size(70, 30);
            this.btThoat.Text = "Thoát";
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(280, 180);
            this.Controls.Add(this.btThoat);
            this.Controls.Add(this.btNhan);
            this.Controls.Add(this.btCong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbKetQua);
            this.Controls.Add(this.tbSoY);
            this.Controls.Add(this.tbSoX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbSoX;
        private System.Windows.Forms.TextBox tbSoY;
        private System.Windows.Forms.TextBox tbKetQua;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCong;
        private System.Windows.Forms.Button btNhan;
        private System.Windows.Forms.Button btThoat;
    }
}
