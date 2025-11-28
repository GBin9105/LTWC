using System;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Gán sự kiện Click cho menu
            bai1GameBongToolStripMenuItem.Click += Bai1_Click;
            bai2TrungRoiToolStripMenuItem.Click += Bai2_Click;
            bai3BatTrungToolStripMenuItem.Click += Bai3_Click;
            bai4GaDeTrungToolStripMenuItem.Click += Bai4_Click;
            bai5ThiTracNghiemToolStripMenuItem.Click += Bai5_Click;
        }

        private void Bai1_Click(object sender, EventArgs e)
        {
            GameBall f = new GameBall();
            f.Show();
        }

        private void Bai2_Click(object sender, EventArgs e)
        {
            EggDrop f = new EggDrop();
            f.Show();
        }

        private void Bai3_Click(object sender, EventArgs e)
        {
            CatchEgg f = new CatchEgg();
            f.Show();
        }

        private void Bai4_Click(object sender, EventArgs e)
        {
            ChickenGame f = new ChickenGame();
            f.Show();
        }

        private void Bai5_Click(object sender, EventArgs e)
        {
            Question f = new Question();
            f.Show();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
