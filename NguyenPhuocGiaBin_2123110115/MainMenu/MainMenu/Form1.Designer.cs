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
            bai6DinoEvolutionToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightSkyBlue;
            menuStrip1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(900, 40);
            menuStrip1.TabIndex = 0;
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bai1GameBongToolStripMenuItem, bai2TrungRoiToolStripMenuItem, bai3BatTrungToolStripMenuItem, bai4GaDeTrungToolStripMenuItem, bai5ThiTracNghiemToolStripMenuItem, bai6DinoEvolutionToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(179, 36);
            menuToolStripMenuItem.Text = "Menu Games";
            // 
            // bai1GameBongToolStripMenuItem
            // 
            bai1GameBongToolStripMenuItem.Font = new Font("Segoe UI", 11F);
            bai1GameBongToolStripMenuItem.Name = "bai1GameBongToolStripMenuItem";
            bai1GameBongToolStripMenuItem.Size = new Size(342, 38);
            bai1GameBongToolStripMenuItem.Text = "Bài 1 - Game Bóng";
            // 
            // bai2TrungRoiToolStripMenuItem
            // 
            bai2TrungRoiToolStripMenuItem.Font = new Font("Segoe UI", 11F);
            bai2TrungRoiToolStripMenuItem.Name = "bai2TrungRoiToolStripMenuItem";
            bai2TrungRoiToolStripMenuItem.Size = new Size(342, 38);
            bai2TrungRoiToolStripMenuItem.Text = "Bài 2 - Trứng Rơi";
            // 
            // bai3BatTrungToolStripMenuItem
            // 
            bai3BatTrungToolStripMenuItem.Font = new Font("Segoe UI", 11F);
            bai3BatTrungToolStripMenuItem.Name = "bai3BatTrungToolStripMenuItem";
            bai3BatTrungToolStripMenuItem.Size = new Size(342, 38);
            bai3BatTrungToolStripMenuItem.Text = "Bài 3 - Bắt Trứng";
            // 
            // bai4GaDeTrungToolStripMenuItem
            // 
            bai4GaDeTrungToolStripMenuItem.Font = new Font("Segoe UI", 11F);
            bai4GaDeTrungToolStripMenuItem.Name = "bai4GaDeTrungToolStripMenuItem";
            bai4GaDeTrungToolStripMenuItem.Size = new Size(342, 38);
            bai4GaDeTrungToolStripMenuItem.Text = "Bài 4 - Gà Đẻ Trứng";
            // 
            // bai5ThiTracNghiemToolStripMenuItem
            // 
            bai5ThiTracNghiemToolStripMenuItem.Font = new Font("Segoe UI", 11F);
            bai5ThiTracNghiemToolStripMenuItem.Name = "bai5ThiTracNghiemToolStripMenuItem";
            bai5ThiTracNghiemToolStripMenuItem.Size = new Size(342, 38);
            bai5ThiTracNghiemToolStripMenuItem.Text = "Bài 5 - Thi Trắc Nghiệm";
            // 
            // bai6DinoEvolutionToolStripMenuItem
            // 
            bai6DinoEvolutionToolStripMenuItem.Font = new Font("Segoe UI", 11F);
            bai6DinoEvolutionToolStripMenuItem.Name = "bai6DinoEvolutionToolStripMenuItem";
            bai6DinoEvolutionToolStripMenuItem.Size = new Size(342, 38);
            bai6DinoEvolutionToolStripMenuItem.Text = "Bài 6 - Dino Transform";
            // 
            // Form1
            // 
            ClientSize = new Size(900, 600);
            Controls.Add(menuStrip1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Menu";
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
        private System.Windows.Forms.ToolStripMenuItem bai6DinoEvolutionToolStripMenuItem;
    }
}
