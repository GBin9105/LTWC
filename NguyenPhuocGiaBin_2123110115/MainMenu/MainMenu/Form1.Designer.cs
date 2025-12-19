using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
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

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem bai1GameBongToolStripMenuItem;
        private ToolStripMenuItem bai2TrungRoiToolStripMenuItem;
        private ToolStripMenuItem bai3BatTrungToolStripMenuItem;
        private ToolStripMenuItem bai4GaDeTrungToolStripMenuItem;
        private ToolStripMenuItem bai5ThiTracNghiemToolStripMenuItem;
        private ToolStripMenuItem bai6DinoEvolutionToolStripMenuItem;
        private ToolStripMenuItem bai7UnfairGameToolStripMenuItem;

        private Label lblTitle;
        private Label lblSubTitle;

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
            bai7UnfairGameToolStripMenuItem = new ToolStripMenuItem();
            lblTitle = new Label();
            lblSubTitle = new Label();

            SuspendLayout();

            // ================= MENU STRIP =================
            menuStrip1.BackColor = Color.FromArgb(30, 30, 30);
            menuStrip1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Dock = DockStyle.Top;
            menuStrip1.Padding = new Padding(20, 10, 20, 10);
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new ModernColorTable());
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });

            // ================= MENU ROOT =================
            menuToolStripMenuItem.Text = "🎮 GAME MENU";
            menuToolStripMenuItem.ForeColor = Color.White;
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                bai1GameBongToolStripMenuItem,
                bai2TrungRoiToolStripMenuItem,
                bai3BatTrungToolStripMenuItem,
                bai4GaDeTrungToolStripMenuItem,
                bai5ThiTracNghiemToolStripMenuItem,
                bai6DinoEvolutionToolStripMenuItem,
                bai7UnfairGameToolStripMenuItem
            });

            // ================= MENU ITEMS =================
            ConfigureItem(bai1GameBongToolStripMenuItem, "🎾  Bài 1 - Game Bóng");
            ConfigureItem(bai2TrungRoiToolStripMenuItem, "🥚  Bài 2 - Trứng Rơi");
            ConfigureItem(bai3BatTrungToolStripMenuItem, "🧺  Bài 3 - Bắt Trứng");
            ConfigureItem(bai4GaDeTrungToolStripMenuItem, "🐔  Bài 4 - Gà Đẻ Trứng");
            ConfigureItem(bai5ThiTracNghiemToolStripMenuItem, "📝  Bài 5 - Trắc Nghiệm");
            ConfigureItem(bai6DinoEvolutionToolStripMenuItem, "🦖  Bài 6 - Dino Transform");
            ConfigureItem(bai7UnfairGameToolStripMenuItem, "😈  Bài 7 - Unfair Game");

            // ================= TITLE =================
            lblTitle.Text = "GAME COLLECTION";
            lblTitle.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(40, 40, 40);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(60, 140);

            lblSubTitle.Text = "Chọn trò chơi trong menu để bắt đầu";
            lblSubTitle.Font = new Font("Segoe UI", 16F);
            lblSubTitle.ForeColor = Color.Gray;
            lblSubTitle.AutoSize = true;
            lblSubTitle.Location = new Point(65, 200);

            // ================= FORM =================
            ClientSize = new Size(1000, 650);
            BackColor = Color.WhiteSmoke;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Menu";

            Controls.Add(lblTitle);
            Controls.Add(lblSubTitle);
            Controls.Add(menuStrip1);

            MainMenuStrip = menuStrip1;
            ResumeLayout(false);
            PerformLayout();
        }

        private void ConfigureItem(ToolStripMenuItem item, string text)
        {
            item.Text = text;
            item.Font = new Font("Segoe UI", 12F);
            item.ForeColor = Color.White;
            item.BackColor = Color.FromArgb(45, 45, 45);
        }

        #endregion
    }

    // ================= CUSTOM RENDERER =================
    class ModernColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(60, 120, 200);
        public override Color MenuItemBorder => Color.Transparent;
        public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 45);
        public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 45);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 45);
        public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 45);
    }
}
