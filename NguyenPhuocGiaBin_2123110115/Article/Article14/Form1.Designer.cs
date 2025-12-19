using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Article14
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox tbName;
        private RadioButton rbMale;
        private RadioButton rbFemale;
        private CheckBox ckDiscount;
        private TextBox tbDiscount;
        private Button btRun;
        private Button btExit;
        private TextBox tbResult;
        private GroupBox groupBox1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tbName = new TextBox();
            this.rbMale = new RadioButton();
            this.rbFemale = new RadioButton();
            this.ckDiscount = new CheckBox();
            this.tbDiscount = new TextBox();
            this.btRun = new Button();
            this.btExit = new Button();
            this.tbResult = new TextBox();
            this.groupBox1 = new GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // tbName
            this.tbName.Location = new System.Drawing.Point(12, 12);
            this.tbName.Size = new System.Drawing.Size(240, 20);

            // groupBox1
            this.groupBox1.Controls.Add(this.rbMale);
            this.groupBox1.Controls.Add(this.rbFemale);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Size = new System.Drawing.Size(240, 50);
            this.groupBox1.Text = "Giới tính";

            // rbMale
            this.rbMale.Location = new System.Drawing.Point(20, 20);
            this.rbMale.Text = "Nam";
            this.rbMale.Checked = true;

            // rbFemale
            this.rbFemale.Location = new System.Drawing.Point(120, 20);
            this.rbFemale.Text = "Nữ";

            // ckDiscount
            this.ckDiscount.Location = new System.Drawing.Point(12, 100);
            this.ckDiscount.Text = "Giảm giá";
            this.ckDiscount.CheckedChanged += new System.EventHandler(this.ckDiscount_CheckedChanged);

            // tbDiscount
            this.tbDiscount.Location = new System.Drawing.Point(100, 98);
            this.tbDiscount.Size = new System.Drawing.Size(50, 20);

            // tbResult
            this.tbResult.Location = new System.Drawing.Point(12, 130);
            this.tbResult.Multiline = true;
            this.tbResult.Size = new System.Drawing.Size(240, 50);

            // btRun
            this.btRun.Location = new System.Drawing.Point(30, 190);
            this.btRun.Text = "Tính tiền";
            this.btRun.Click += new System.EventHandler(this.btRun_Click);

            // btExit
            this.btExit.Location = new System.Drawing.Point(140, 190);
            this.btExit.Text = "Thoát";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(270, 230);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ckDiscount);
            this.Controls.Add(this.tbDiscount);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.btExit);
            this.Text = "Article 14";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
