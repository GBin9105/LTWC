using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class ChickenGame : Form
    {
        Random rnd = new Random();
        List<PictureBox> eggs = new List<PictureBox>();

        int chickenSpeed = 4;
        int score = 0;
        int miss = 0;

        int layDelay = 0;
        int layDelayMax = 30;

        public ChickenGame()
        {
            InitializeComponent();
        }

        private void ChickenGame_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveChicken();

            layDelay++;
            if (layDelay >= layDelayMax)
            {
                LayEggs();
                layDelay = 0;
                layDelayMax = rnd.Next(20, 50);
            }

            MoveEggs();
        }

        private void MoveChicken()
        {
            pbChicken.Left += chickenSpeed;

            if (pbChicken.Left <= 0 || pbChicken.Right >= ClientSize.Width)
                chickenSpeed = -chickenSpeed;
        }

        private void LayEggs()
        {
            int eggCount = rnd.Next(1, 4);

            for (int i = 0; i < eggCount; i++)
            {
                PictureBox egg = new PictureBox
                {
                    BackColor = Color.White,
                    Size = new Size(25, 25)
                };

                int ex = pbChicken.Left + pbChicken.Width / 2 - 12 + rnd.Next(-15, 15);
                int ey = pbChicken.Bottom;

                egg.Location = new Point(ex, ey);

                eggs.Add(egg);
                Controls.Add(egg);
                egg.BringToFront();
            }
        }

        private void MoveEggs()
        {
            List<PictureBox> toRemove = new List<PictureBox>();

            foreach (var egg in eggs)
            {
                egg.Top += 6;

                if (egg.Bounds.IntersectsWith(pbBasket.Bounds))
                {
                    score++;
                    lblScore.Text = "Điểm: " + score;
                    toRemove.Add(egg);
                    continue;
                }

                if (egg.Top >= ClientSize.Height - egg.Height)
                {
                    miss++;
                    lblMiss.Text = "Rơi Mất: " + miss;

                    if (miss >= 3)
                    {
                        timer1.Stop();
                        MessageBox.Show("GAME OVER! Bạn đã để rơi 3 quả trứng!");
                        Close();
                        return;
                    }

                    toRemove.Add(egg);
                }
            }

            foreach (var egg in toRemove)
            {
                Controls.Remove(egg);
                eggs.Remove(egg);
                egg.Dispose();
            }
        }

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
