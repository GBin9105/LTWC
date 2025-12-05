using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    partial class DinoEvolution
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Timer gameTimer;
        private PictureBox player;
        private PictureBox ground;
        private Label lblScore;
        private Label lblSpeed;
        private Label lblMode;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// UI Initialization
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.player = new System.Windows.Forms.PictureBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.ground = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // gameTimer
            // ============================================================
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);

            // ============================================================
            // PLAYER
            // ============================================================
            this.player.BackColor = Color.LimeGreen;
            this.player.Size = new Size(80, 80);
            this.player.Location = new Point(200, 300);
            this.player.SizeMode = PictureBoxSizeMode.StretchImage;
            this.player.Name = "player";

            // ============================================================
            // lblScore
            // ============================================================
            this.lblScore.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblScore.ForeColor = Color.Black;
            this.lblScore.Location = new Point(30, 30);
            this.lblScore.Size = new Size(300, 60);
            this.lblScore.Text = "Điểm: 0";
            this.lblScore.Name = "lblScore";

            // ============================================================
            // lblSpeed
            // ============================================================
            this.lblSpeed.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblSpeed.ForeColor = Color.Black;
            this.lblSpeed.Location = new Point(350, 30);
            this.lblSpeed.Size = new Size(350, 60);
            this.lblSpeed.Text = "Tốc độ: 0";
            this.lblSpeed.Name = "lblSpeed";

            // ============================================================
            // lblMode
            // ============================================================
            this.lblMode.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblMode.ForeColor = Color.Black;
            this.lblMode.Location = new Point(750, 30);
            this.lblMode.Size = new Size(500, 60);
            this.lblMode.Text = "Chế độ: T-Rex";
            this.lblMode.Name = "lblMode";

            // ============================================================
            // ground
            // ============================================================
            this.ground.BackColor = Color.SaddleBrown;
            this.ground.Dock = DockStyle.Bottom;
            this.ground.Height = 80;
            this.ground.Name = "ground";

            // ============================================================
            // FORM SETTINGS
            // ============================================================
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.player);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.ground);

            this.KeyPreview = true;
            this.Name = "DinoEvolution";
            this.Text = "Dino Evolution";

            this.Load += new System.EventHandler(this.DinoEvolution_Load);
            this.KeyDown += new KeyEventHandler(this.DinoEvolution_KeyDown);
            this.KeyUp += new KeyEventHandler(this.DinoEvolution_KeyUp);

            // bảo đảm label nằm trên vật cản
            this.lblScore.BringToFront();
            this.lblSpeed.BringToFront();
            this.lblMode.BringToFront();

            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
