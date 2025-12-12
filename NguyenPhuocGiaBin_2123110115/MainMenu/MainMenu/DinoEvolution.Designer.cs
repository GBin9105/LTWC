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

        private Panel hudPanel;
        private Label lblScore;
        private Label lblLives;
        private Label lblSpeed;
        private Label lblMode;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.player = new PictureBox();
            this.ground = new PictureBox();
            this.hudPanel = new Panel();
            this.lblScore = new Label();
            this.lblLives = new Label();
            this.lblSpeed = new Label();
            this.lblMode = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            this.SuspendLayout();

            // ==========================================================================
            // GAME TIMER (FPS BOOST)
            // ==========================================================================
            this.gameTimer.Interval = 8;                     // ~120 FPS
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);

            // ==========================================================================
            // HUD PANEL
            // ==========================================================================
            this.hudPanel.BackColor = Color.FromArgb(30, 30, 30);
            this.hudPanel.Dock = DockStyle.Top;
            this.hudPanel.Height = 80;
            this.hudPanel.Padding = new Padding(20, 10, 20, 10);
            this.hudPanel.Name = "hudPanel";

            // LABEL: SCORE
            this.lblScore.ForeColor = Color.White;
            this.lblScore.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new Point(20, 20);
            this.lblScore.Name = "lblScore";
            this.lblScore.Text = "Điểm: 0";

            // LABEL: LIVES
            this.lblLives.ForeColor = Color.White;
            this.lblLives.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblLives.AutoSize = true;
            this.lblLives.Location = new Point(300, 20);
            this.lblLives.Name = "lblLives";
            this.lblLives.Text = "Mạng: 3";

            // LABEL: SPEED
            this.lblSpeed.ForeColor = Color.White;
            this.lblSpeed.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new Point(600, 20);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Text = "Tốc độ: 10";

            // LABEL: MODE
            this.lblMode.ForeColor = Color.White;
            this.lblMode.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new Point(900, 20);
            this.lblMode.Name = "lblMode";
            this.lblMode.Text = "Chế độ: T-Rex";

            this.hudPanel.Controls.Add(this.lblScore);
            this.hudPanel.Controls.Add(this.lblLives);
            this.hudPanel.Controls.Add(this.lblSpeed);
            this.hudPanel.Controls.Add(this.lblMode);

            // ==========================================================================
            // PLAYER
            // ==========================================================================
            this.player.BackColor = Color.LimeGreen;
            this.player.Size = new Size(80, 80);
            this.player.Location = new Point(300, 400);
            this.player.Name = "player";
            this.player.SizeMode = PictureBoxSizeMode.StretchImage;

            // ==========================================================================
            // GROUND
            // ==========================================================================
            this.ground.BackColor = Color.SaddleBrown;
            this.ground.Dock = DockStyle.Bottom;
            this.ground.Height = 100;
            this.ground.Name = "ground";

            // ==========================================================================
            // FORM SETTINGS
            // ==========================================================================
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1600, 900);

            this.Controls.Add(this.player);
            this.Controls.Add(this.hudPanel);
            this.Controls.Add(this.ground);

            this.KeyPreview = true;
            this.Name = "DinoEvolution";
            this.Text = "Dino Evolution";

            this.Load += new System.EventHandler(this.DinoEvolution_Load);
            this.KeyDown += new KeyEventHandler(this.DinoEvolution_KeyDown);
            this.KeyUp += new KeyEventHandler(this.DinoEvolution_KeyUp);

            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
