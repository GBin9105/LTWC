namespace MainMenu
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
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            bai1GameBongToolStripMenuItem = new ToolStripMenuItem();
            bai2TrungRoiToolStripMenuItem = new ToolStripMenuItem();
            bai3BatTrungToolStripMenuItem = new ToolStripMenuItem();
            bai4GaDeTrungToolStripMenuItem = new ToolStripMenuItem();
            bai5ThiTracNghiemToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bai1GameBongToolStripMenuItem, bai2TrungRoiToolStripMenuItem, bai3BatTrungToolStripMenuItem, bai4GaDeTrungToolStripMenuItem, bai5ThiTracNghiemToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(57, 20);
            menuToolStripMenuItem.Text = "Bài Tập";
            // 
            // bai1GameBongToolStripMenuItem
            // 
            bai1GameBongToolStripMenuItem.Name = "bai1GameBongToolStripMenuItem";
            bai1GameBongToolStripMenuItem.Size = new Size(196, 22);
            bai1GameBongToolStripMenuItem.Text = "Bài 1 - Game Bóng";
            bai1GameBongToolStripMenuItem.Click += Bai1_Click;
            // 
            // bai2TrungRoiToolStripMenuItem
            // 
            bai2TrungRoiToolStripMenuItem.Name = "bai2TrungRoiToolStripMenuItem";
            bai2TrungRoiToolStripMenuItem.Size = new Size(196, 22);
            bai2TrungRoiToolStripMenuItem.Text = "Bài 2 - Trứng Rơi";
            bai2TrungRoiToolStripMenuItem.Click += Bai2_Click;
            // 
            // bai3BatTrungToolStripMenuItem
            // 
            bai3BatTrungToolStripMenuItem.Name = "bai3BatTrungToolStripMenuItem";
            bai3BatTrungToolStripMenuItem.Size = new Size(196, 22);
            bai3BatTrungToolStripMenuItem.Text = "Bài 3 - Bắt Trứng";
            bai3BatTrungToolStripMenuItem.Click += Bai3_Click;
            // 
            // bai4GaDeTrungToolStripMenuItem
            // 
            bai4GaDeTrungToolStripMenuItem.Name = "bai4GaDeTrungToolStripMenuItem";
            bai4GaDeTrungToolStripMenuItem.Size = new Size(196, 22);
            bai4GaDeTrungToolStripMenuItem.Text = "Bài 4 - Gà Đẻ Trứng";
            bai4GaDeTrungToolStripMenuItem.Click += Bai4_Click;
            // 
            // bai5ThiTracNghiemToolStripMenuItem
            // 
            bai5ThiTracNghiemToolStripMenuItem.Name = "bai5ThiTracNghiemToolStripMenuItem";
            bai5ThiTracNghiemToolStripMenuItem.Size = new Size(196, 22);
            bai5ThiTracNghiemToolStripMenuItem.Text = "Bài 5 - Thi Trắc Nghiệm";
            bai5ThiTracNghiemToolStripMenuItem.Click += Bai5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Main Menu";
            Load += Form1_Load_1;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bai1GameBongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bai2TrungRoiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bai3BatTrungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bai4GaDeTrungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bai5ThiTracNghiemToolStripMenuItem;
    }
}
