using System;
using System.Windows.Forms;

namespace Article12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // FORM LOAD → CHỌN SẴN ITEM
        private void Form1_Load(object sender, EventArgs e)
        {
            cb_Faculty.SelectedIndex = 2; // Quản trị kinh doanh
        }

        // THAY ĐỔI COMBOBOX
        private void cb_Faculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cb_Faculty.SelectedIndex;
            tbDisplay.Text = "Bạn đã chọn khoa thứ: " + index.ToString();
        }

        // BUTTON OK
        private void btOK_Click(object sender, EventArgs e)
        {
            if (cb_Faculty.SelectedItem != null)
            {
                string item = cb_Faculty.SelectedItem.ToString();
                tbDisplay.Text = "Bạn là sinh viên khoa: " + item;
            }
        }

        // BUTTON CLEAR
        private void btClear_Click(object sender, EventArgs e)
        {
            tbDisplay.Clear();
            cb_Faculty.SelectedIndex = -1;
        }
    }
}
