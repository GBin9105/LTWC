using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class GameBall : Form
    {
        int ballX = 4, ballY = 4;       // Tốc độ bóng
        int paddleSpeed = 7;            // Tốc độ thanh đỡ
        bool moveLeft = false, moveRight = false;
        int score = 0;

        public GameBall()
        {
            InitializeComponent();
            this.KeyPreview = true;  // Cho phép form nhận sự kiện phím
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Bóng di chuyển
            ball.Left += ballX;
            ball.Top += ballY;

            // Va chạm viền trái/phải
            if (ball.Left <= 0 || ball.Right >= ClientSize.Width)
                ballX = -ballX;

            // Va chạm trên
            if (ball.Top <= 0)
                ballY = -ballY;

            // Va chạm thanh đỡ
            if (ball.Bounds.IntersectsWith(paddle.Bounds))
            {
                ballY = -ballY;
                score++;
                lblScore.Text = "Điểm: " + score;
            }

            // Game over
            if (ball.Top >= ClientSize.Height)
            {
                timer1.Stop();
                MessageBox.Show("Thua rồi!\nĐiểm: " + score);
            }

            // Điều khiển thanh đỡ mượt
            if (moveLeft && paddle.Left > 0)
                paddle.Left -= paddleSpeed;

            if (moveRight && paddle.Right < ClientSize.Width)
                paddle.Left += paddleSpeed;
        }

        private void GameBall_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = true;
            if (e.KeyCode == Keys.Right) moveRight = true;
        }

        private void GameBall_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = false;
            if (e.KeyCode == Keys.Right) moveRight = false;
        }
    }
}
