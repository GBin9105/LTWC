using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MainMenu
{
    public partial class LoginForm : Form
    {
        public static string PlayerName = "";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên!");
                return;
            }

            PlayerName = txtName.Text.Trim();   // Lưu tên người chơi

            Question qForm = new Question();
            qForm.Show();
            this.Hide();
        }
    }
}
