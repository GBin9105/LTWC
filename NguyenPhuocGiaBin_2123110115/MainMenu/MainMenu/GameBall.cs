using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class GameBall : Form
    {
        // ================= CONFIG =================
        private int ballSpeed = 10;
        private int ballVx;
        private int ballVy;

        // Paddle speed = ballSpeed + 10
        private int paddleSpeed => ballSpeed + 10;

        // ================= STATE =================
        private bool moveLeft = false;
        private bool moveRight = false;
        private int score = 0;

        private readonly Random rand = new();

        public GameBall()
        {
            InitializeComponent();

            KeyPreview = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            if (timer1 != null)
                timer1.Interval = 15; // ~66 FPS
        }

        private void GameBall_Load(object sender, EventArgs e)
        {
            ResetGame();
            timer1?.Start();
        }

        // =========================================================
        // GAME LOOP
        // =========================================================
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ball == null || paddle == null) return;

            // ================= BALL MOVE =================
            ball.Left += ballVx;
            ball.Top += ballVy;

            // Left / Right wall
            if (ball.Left <= 0)
            {
                ball.Left = 0;
                ballVx = Math.Abs(ballVx);
            }
            else if (ball.Right >= ClientSize.Width)
            {
                ball.Left = ClientSize.Width - ball.Width;
                ballVx = -Math.Abs(ballVx);
            }

            // Top wall
            if (ball.Top <= 0)
            {
                ball.Top = 0;
                ballVy = Math.Abs(ballVy);
            }

            // ================= PADDLE COLLISION =================
            if (ball.Bounds.IntersectsWith(paddle.Bounds) && ballVy > 0)
            {
                ballVy = -Math.Abs(ballVy);

                score++;
                if (lblScore != null)
                    lblScore.Text = "Điểm: " + score;

                // tăng tốc
                ballSpeed += 2;

                int signX = Math.Sign(ballVx);
                if (signX == 0) signX = 1;

                ballVx = signX * ballSpeed;
                ballVy = -ballSpeed;
            }

            // ================= GAME OVER =================
            if (ball.Top >= ClientSize.Height)
            {
                timer1?.Stop();

                // ===== FIX BUG AUTO MOVE =====
                moveLeft = false;
                moveRight = false;
                // ============================

                MessageBox.Show(
                    $"Thua rồi!\nĐiểm: {score}",
                    "Game Over",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // ================= PADDLE MOVE =================
            if (moveLeft && paddle.Left > 0)
                paddle.Left = Math.Max(0, paddle.Left - paddleSpeed);

            if (moveRight && paddle.Right < ClientSize.Width)
                paddle.Left = Math.Min(ClientSize.Width - paddle.Width, paddle.Left + paddleSpeed);
        }

        // =========================================================
        // INPUT
        // =========================================================
        private void GameBall_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = true;
            if (e.KeyCode == Keys.Right) moveRight = true;

            // Restart
            if (e.KeyCode == Keys.Space)
            {
                if (timer1 != null && !timer1.Enabled)
                {
                    ResetGame();
                    timer1.Start();
                }
            }

            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void GameBall_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = false;
            if (e.KeyCode == Keys.Right) moveRight = false;
        }

        // =========================================================
        // RESET GAME (FIX INPUT STATE)
        // =========================================================
        private void ResetGame()
        {
            // ===== FIX BUG AUTO MOVE =====
            moveLeft = false;
            moveRight = false;
            // ============================

            score = 0;
            ballSpeed = 10;

            if (lblScore != null)
                lblScore.Text = "Điểm: 0";

            // Paddle center bottom
            if (paddle != null)
            {
                paddle.Left = (ClientSize.Width - paddle.Width) / 2;
                paddle.Top = ClientSize.Height - paddle.Height - 30;
            }

            // Ball center
            if (ball != null)
            {
                ball.Left = (ClientSize.Width - ball.Width) / 2;
                ball.Top = (ClientSize.Height - ball.Height) / 2;

                int sx = rand.Next(0, 2) == 0 ? -1 : 1;
                ballVx = sx * ballSpeed;
                ballVy = -ballSpeed;
            }
        }
    }
}
