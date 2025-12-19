using System;
using System.Windows.Forms;

namespace Article11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cbFaculty.Items.Add("Công nghệ thông tin");
            cbFaculty.Items.Add("Kế toán");
            cbFaculty.Items.Add("Cơ khí");
            cbFaculty.Items.Add("Điện");
            cbFaculty.Items.Add("Hóa");

            cbFaculty.SelectedIndex = 0; // chọn mặc định
        }

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = cbFaculty.SelectedItem.ToString();
            MessageBox.Show("Bạn chọn: " + value);
        }
    }
}
