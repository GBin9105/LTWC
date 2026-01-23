using System;
using System.Windows.Forms;
using QLSV.BLL;

namespace QLSV
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth = new AuthService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var u = _auth.Login(txtUsername.Text, txtPassword.Text);
                if (u == null)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu.");
                    return;
                }

                var main = new MainForm(u);
                main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
        }

        // CHẠY 1 LẦN để tạo admin rồi comment lại
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    _auth.CreateUser("admin", "123456", "ADMIN");
            //    MessageBox.Show("Đã tạo admin: admin / 123456. Hãy comment lại dòng CreateUser!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Tạo admin lỗi: " + ex.Message);
            //}
        }
    }
}
