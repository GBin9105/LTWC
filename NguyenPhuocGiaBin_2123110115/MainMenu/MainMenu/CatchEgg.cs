using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class CatchEgg : Form
    {
        Random rnd = new Random();

        // Vị trí trứng
        int y1, y2, y3;
        int x1, x2, x3;

        // Tốc độ trứng
        int s1, s2, s3;

        int score = 0;

        public CatchEgg()
        {
            InitializeComponent();
        }

        private void CatchEgg_Load(object sender, EventArgs e)
        {
            ResetEggs();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DropEgg(ref y1, ref x1, ref s1, pbEgg1);
            DropEgg(ref y2, ref x2, ref s2, pbEgg2);
            DropEgg(ref y3, ref x3, ref s3, pbEgg3);
        }

        private void DropEgg(ref int y, ref int x, ref int speed, PictureBox egg)
        {
            y += speed;

            // Nếu chạm đất → GAME OVER
            if (y >= ClientSize.Height - egg.Height)
            {
                timer1.Stop();
                MessageBox.Show("GAME OVER!\nBạn để rơi trứng!", "Thua");
                Close();
                return;
            }

            // Nếu bắt được trứng
            if (egg.Bounds.IntersectsWith(pbBasket.Bounds))
            {
                score++;
                lblScore.Text = "Điểm: " + score;
                Reset1Egg(ref y, ref x, ref speed, egg);
                return;
            }

            egg.Location = new Point(x, y);
        }

        private void ResetEggs()
        {
            Reset1Egg(ref y1, ref x1, ref s1, pbEgg1);
            Reset1Egg(ref y2, ref x2, ref s2, pbEgg2);
            Reset1Egg(ref y3, ref x3, ref s3, pbEgg3);
        }

        private void Reset1Egg(ref int y, ref int x, ref int speed, PictureBox egg)
        {
            y = rnd.Next(-200, -50); // random cao thấp
            x = rnd.Next(0, ClientSize.Width - egg.Width); // random vị trí ngang
            speed = rnd.Next(4, 9); // tốc độ random

            egg.Location = new Point(x, y);
            egg.BackColor = Color.White;
        }

        // Điều khiển giỏ bằng phím
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int moveSpeed = 20;

            if (keyData == Keys.Left)
            {
                pbBasket.Left -= moveSpeed;
                if (pbBasket.Left < 0) pbBasket.Left = 0;
                return true;
            }
            if (keyData == Keys.Right)
            {
                pbBasket.Left += moveSpeed;
                if (pbBasket.Right > ClientSize.Width)
                    pbBasket.Left = ClientSize.Width - pbBasket.Width;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
