namespace Article10
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox tbDisplay;
        private System.Windows.Forms.Button bt0;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.Button bt3;
        private System.Windows.Forms.Button btPlus;
        private System.Windows.Forms.Button btMul;
        private System.Windows.Forms.Button btEquals;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tbDisplay = new System.Windows.Forms.TextBox();
            this.bt0 = new System.Windows.Forms.Button();
            this.bt1 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt3 = new System.Windows.Forms.Button();
            this.btPlus = new System.Windows.Forms.Button();
            this.btMul = new System.Windows.Forms.Button();
            this.btEquals = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDisplay
            // 
            this.tbDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.tbDisplay.Location = new System.Drawing.Point(12, 12);
            this.tbDisplay.Name = "tbDisplay";
            this.tbDisplay.Size = new System.Drawing.Size(260, 41);
            this.tbDisplay.TabIndex = 0;
            this.tbDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bt0
            // 
            this.bt0.Location = new System.Drawing.Point(12, 70);
            this.bt0.Name = "bt0";
            this.bt0.Size = new System.Drawing.Size(60, 50);
            this.bt0.Text = "0";
            this.bt0.Click += new System.EventHandler(this.bt0_Click);
            // 
            // bt1
            // 
            this.bt1.Location = new System.Drawing.Point(78, 70);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(60, 50);
            this.bt1.Text = "1";
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            // 
            // bt2
            // 
            this.bt2.Location = new System.Drawing.Point(144, 70);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(60, 50);
            this.bt2.Text = "2";
            this.bt2.Click += new System.EventHandler(this.bt2_Click);
            // 
            // bt3
            // 
            this.bt3.Location = new System.Drawing.Point(210, 70);
            this.bt3.Name = "bt3";
            this.bt3.Size = new System.Drawing.Size(60, 50);
            this.bt3.Text = "3";
            this.bt3.Click += new System.EventHandler(this.bt3_Click);
            // 
            // btPlus
            // 
            this.btPlus.Location = new System.Drawing.Point(12, 130);
            this.btPlus.Name = "btPlus";
            this.btPlus.Size = new System.Drawing.Size(60, 50);
            this.btPlus.Text = "+";
            this.btPlus.Click += new System.EventHandler(this.btPlus_Click);
            // 
            // btMul
            // 
            this.btMul.Location = new System.Drawing.Point(78, 130);
            this.btMul.Name = "btMul";
            this.btMul.Size = new System.Drawing.Size(60, 50);
            this.btMul.Text = "*";
            this.btMul.Click += new System.EventHandler(this.btMul_Click);
            // 
            // btEquals
            // 
            this.btEquals.Location = new System.Drawing.Point(144, 130);
            this.btEquals.Name = "btEquals";
            this.btEquals.Size = new System.Drawing.Size(126, 50);
            this.btEquals.Text = "=";
            this.btEquals.Click += new System.EventHandler(this.btEquals_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 200);
            this.Controls.Add(this.btEquals);
            this.Controls.Add(this.btMul);
            this.Controls.Add(this.btPlus);
            this.Controls.Add(this.bt3);
            this.Controls.Add(this.bt2);
            this.Controls.Add(this.bt1);
            this.Controls.Add(this.bt0);
            this.Controls.Add(this.tbDisplay);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
