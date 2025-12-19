using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Article07
{
    public partial class Article07 : Form
    {
        public Article07()
        {
            InitializeComponent();
        }

        // CHỈ CHO NHẬP SỐ + CHO PHÉP BACKSPACE
        private void tbYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // chặn ký tự
            }
        }

        // KIỂM TRA NĂM (VALIDATING)
        private void tbYear_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbYear.Text))
                return;

            int year = int.Parse(tbYear.Text);
            if (year > 2000)
            {
                MessageBox.Show("Year must be <= 2000", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // KHÔNG cho rời TextBox
            }
        }
    }
}
