using System;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Gán sự kiện Click cho menu
            bai1GameBongToolStripMenuItem.Click += Bai1_Click;
            bai2TrungRoiToolStripMenuItem.Click += Bai2_Click;
            bai3BatTrungToolStripMenuItem.Click += Bai3_Click;
            bai4GaDeTrungToolStripMenuItem.Click += Bai4_Click;
            bai5ThiTracNghiemToolStripMenuItem.Click += Bai5_Click;

            // Thêm Game 6
            bai6DinoEvolutionToolStripMenuItem.Click += Bai6_Click;
        }

        private void Bai1_Click(object sender, EventArgs e)
        {
            new GameBall().Show();
        }

        private void Bai2_Click(object sender, EventArgs e)
        {
            new EggDrop().Show();
        }

        private void Bai3_Click(object sender, EventArgs e)
        {
            new CatchEgg().Show();
        }

        private void Bai4_Click(object sender, EventArgs e)
        {
            new ChickenGame().Show();
        }

        private void Bai5_Click(object sender, EventArgs e)
        {
            new Question().Show();
        }

        private void Bai6_Click(object sender, EventArgs e)
        {
            new DinoEvolution().Show();
        }
    }
}
