using System;
using System.IO;
using System.Windows.Forms;

namespace Article09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btCong_Click(object sender, EventArgs e)
        {
            int x = int.Parse(tbSoX.Text);
            int y = int.Parse(tbSoY.Text);
            int kq = x + y;

            tbKetQua.Text += $"{x} + {y} = {kq}\r\n";
        }

        private void btNhan_Click(object sender, EventArgs e)
        {
            int x = int.Parse(tbSoX.Text);
            int y = int.Parse(tbSoY.Text);
            int kq = x * y;

            tbKetQua.Text += $"{x} * {y} = {kq}\r\n";
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("Calculator.txt", true))
            {
                sw.Write(tbKetQua.Text);
            }

            MessageBox.Show("Đã lưu vào Calculator.txt");
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
