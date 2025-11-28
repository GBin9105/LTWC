using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class EggDrop : Form
    {
        int yEgg = 0;
        int xEgg = 0;
        int speed = 6;
        int score = 0;

        Random rnd = new Random();
        int chickenSpeed = 4;

        public EggDrop()
        {
            InitializeComponent();
        }

        private void EggDrop_Load(object sender, EventArgs e)
        {
            ResetEgg();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveChicken();

            // Trứng rơi
            yEgg += speed;

            // Nếu trứng rơi xuống đất -> THUA
            if (yEgg >= ClientSize.Height - pbEgg.Height)
            {
                timer1.Stop();
                MessageBox.Show("GAME OVER!\nBạn bị rơi mất trứng!", "Thua rồi");
                Close();
                return;
            }

            // Bắt trứng
            if (pbEgg.Bounds.IntersectsWith(pbBasket.Bounds))
            {
                score++;
                lblScore.Text = "Điểm: " + score;

                pbEgg.BackColor = Color.Gold; // trứng vàng khi bắt trúng
                ResetEgg();
            }

            // Cập nhật vị trí trứng
            pbEgg.Location = new Point(xEgg, yEgg);
        }

        private void MoveChicken()
        {
            // Gà di chuyển trái → phải liên tục
            pbChicken.Left += chickenSpeed;

            // Va tường → đổi hướng
            if (pbChicken.Left <= 0 || pbChicken.Right >= ClientSize.Width)
                chickenSpeed = -chickenSpeed;
        }

        private void ResetEgg()
        {
            // Lấy vị trí mới từ vị trí hiện tại của gà
            yEgg = pbChicken.Bottom;
            xEgg = pbChicken.Left + pbChicken.Width / 2 - pbEgg.Width / 2;

            pbEgg.Location = new Point(xEgg, yEgg);
            pbEgg.BackColor = Color.White; // trứng bình thường
        }

        // Điều khiển giỏ
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
