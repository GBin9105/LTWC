using System;
using System.Windows.Forms;

namespace Article14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbDiscount.Enabled = false;
        }

        private void ckDiscount_CheckedChanged(object sender, EventArgs e)
        {
            tbDiscount.Enabled = ckDiscount.Checked;
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            string msg = "";
            int disc = 0;

            if (rbMale.Checked)
                msg = "Ông ";
            else if (rbFemale.Checked)
                msg = "Bà ";

            if (ckDiscount.Checked)
            {
                if (!int.TryParse(tbDiscount.Text, out disc))
                {
                    MessageBox.Show("Vui lòng nhập % giảm hợp lệ");
                    tbDiscount.Focus();
                    return;
                }
            }

            tbResult.Text = msg + tbName.Text +
                            " được giảm " + disc + "%";
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
