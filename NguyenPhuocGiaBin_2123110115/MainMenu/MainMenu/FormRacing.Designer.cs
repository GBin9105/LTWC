// FormRacing.Designer.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    partial class FormRacing
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Timer gameTimer;
        private Panel roadPanel;
        private PictureBox playerCar;
        private Panel hudPanel;
        private Label lblScore;
        private Label lblLives;
        private Label lblSpeed;   // giữ để tránh lỗi tham chiếu từ code-behind
        private Button btnRestart;
        private Button btnBack;
        private Label lblGameOver;

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
        /// Initialize components (Designer)
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            gameTimer = new System.Windows.Forms.Timer(components);

            roadPanel = new Panel();
            playerCar = new PictureBox();
            hudPanel = new Panel();
            lblScore = new Label();
            lblLives = new Label();
            lblSpeed = new Label();
            btnRestart = new Button();
            btnBack = new Button();
            lblGameOver = new Label();

            // ================================
            // gameTimer
            // ================================
            gameTimer.Interval = 20;  // ~50 fps
            gameTimer.Tick += gameTimer_Tick;

            // ================================
            // hudPanel
            // ================================
            hudPanel.SuspendLayout();
            hudPanel.BackColor = Color.FromArgb(30, 30, 30);
            hudPanel.Dock = DockStyle.Top;
            hudPanel.Height = 80;
            hudPanel.Padding = new Padding(12);

            // lblScore
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblScore.ForeColor = Color.White;
            lblScore.Location = new Point(16, 18);
            lblScore.Text = "Điểm: 0";

            // lblLives
            lblLives.AutoSize = true;
            lblLives.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLives.ForeColor = Color.White;
            lblLives.Location = new Point(180, 18);
            lblLives.Text = "Mạng: 3";

            // lblSpeed (giữ control để tránh lỗi tham chiếu từ code-behind)
            lblSpeed.AutoSize = true;
            lblSpeed.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSpeed.ForeColor = Color.White;
            lblSpeed.Location = new Point(320, 18);
            lblSpeed.Text = "Vận tốc: 6";

            // btnRestart
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.BackColor = Color.FromArgb(70, 130, 180);
            btnRestart.ForeColor = Color.White;
            btnRestart.Text = "Restart";
            btnRestart.Size = new Size(110, 36);
            btnRestart.Location = new Point(540, 20);
            btnRestart.Click += btnRestart_Click;

            // btnBack
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.BackColor = Color.FromArgb(90, 90, 90);
            btnBack.ForeColor = Color.White;
            btnBack.Text = "Back";
            btnBack.Size = new Size(110, 36);
            btnBack.Location = new Point(660, 20);
            btnBack.Click += btnBack_Click;

            hudPanel.Controls.Add(lblScore);
            hudPanel.Controls.Add(lblLives);
            hudPanel.Controls.Add(lblSpeed);
            hudPanel.Controls.Add(btnRestart);
            hudPanel.Controls.Add(btnBack);
            hudPanel.ResumeLayout(false);
            hudPanel.PerformLayout();

            // ================================
            // roadPanel
            // ================================
            roadPanel.BackColor = Color.FromArgb(40, 40, 45);
            roadPanel.Dock = DockStyle.Fill;
            roadPanel.Paint += roadPanel_Paint;

            // ================================
            // playerCar
            // ================================
            playerCar.BackColor = Color.LimeGreen;
            playerCar.BorderStyle = BorderStyle.FixedSingle;
            playerCar.SizeMode = PictureBoxSizeMode.StretchImage;
            playerCar.Size = new Size(64, 96);
            playerCar.Location = new Point(418, 520); // will be repositioned on load/resize
            playerCar.TabStop = false;

            // ================================
            // lblGameOver
            // ================================
            lblGameOver.BackColor = Color.FromArgb(180, Color.Black);
            lblGameOver.ForeColor = Color.White;
            lblGameOver.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblGameOver.Size = new Size(420, 120);
            lblGameOver.Text = "Game Over";
            lblGameOver.TextAlign = ContentAlignment.MiddleCenter;
            lblGameOver.Visible = false;

            // ================================
            // FormRacing (this)
            // ================================
            this.SuspendLayout();
            this.ClientSize = new Size(900, 700);
            this.BackColor = Color.Black;
            this.Text = "Racing - Game 7";
            this.StartPosition = FormStartPosition.CenterParent;
            this.KeyPreview = true;

            // Hook events (ensure handlers exist in code-behind)
            this.Load += FormRacing_Load;
            this.KeyDown += FormRacing_KeyDown;
            this.KeyUp += FormRacing_KeyUp;

            // use new unique handler name to avoid duplicate conflicts
            this.Resize += FormRacing_OnResize;

            // Add main controls (order matters for z-index)
            this.Controls.Add(roadPanel);
            this.Controls.Add(hudPanel);
            this.Controls.Add(playerCar);
            this.Controls.Add(lblGameOver);

            // initial layout helper call
            CenterPlayerAndGameOver();

            this.ResumeLayout(false);
        }

        // Ensure handler name used in designer does exist here and will not conflict
        private void FormRacing_OnResize(object sender, EventArgs e)
        {
            CenterPlayerAndGameOver();
        }

        // Helper layout: center player & GameOver label
        private void CenterPlayerAndGameOver()
        {
            if (roadPanel != null && playerCar != null)
            {
                int x = roadPanel.Left + (roadPanel.Width - playerCar.Width) / 2;
                int y = roadPanel.Bottom - playerCar.Height - 12;
                playerCar.Left = Math.Max(roadPanel.Left + 8, x);
                playerCar.Top = y;
            }

            if (lblGameOver != null)
            {
                lblGameOver.Left = (this.ClientSize.Width - lblGameOver.Width) / 2;
                lblGameOver.Top = (this.ClientSize.Height - lblGameOver.Height) / 2;
            }
        }
    }
}
