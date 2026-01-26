using System;
using System.IO;
using System.Windows.Forms;
using QLSV.BLL;

namespace QLSV
{
    public partial class RegisterForm : Form
    {
        private readonly AuthService _auth = new AuthService();
        private string _avatarRelativePath = null; // ví dụ: avatars\abc.png

        // để LoginForm lấy username vừa đăng ký (không còn đỏ UsernameRegistered)
        public string UsernameRegistered { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện";
                ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp|All files|*.*";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // copy ảnh vào thư mục avatars cạnh exe
                    string avatarsDir = Path.Combine(Application.StartupPath, "avatars");
                    if (!Directory.Exists(avatarsDir))
                        Directory.CreateDirectory(avatarsDir);

                    string ext = Path.GetExtension(ofd.FileName);
                    string fileName = Guid.NewGuid().ToString("N") + ext;
                    string destFullPath = Path.Combine(avatarsDir, fileName);

                    File.Copy(ofd.FileName, destFullPath, true);

                    _avatarRelativePath = Path.Combine("avatars", fileName);

                    // hiển thị preview
                    picAvatar.ImageLocation = destFullPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không chọn được avatar: " + ex.Message);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string username = (txtUsername.Text ?? "").Trim();
                string password = txtPassword.Text ?? "";
                string confirm = txtConfirm.Text ?? "";

                if (username.Length == 0)
                {
                    MessageBox.Show("Username không được trống.");
                    txtUsername.Focus();
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu tối thiểu 6 ký tự.");
                    txtPassword.Focus();
                    return;
                }

                if (password != confirm)
                {
                    MessageBox.Show("Mật khẩu nhập lại không khớp.");
                    txtConfirm.Focus();
                    return;
                }

                // chỉ đăng ký tài khoản STUDENT + lưu avatar path (nếu có)
                _auth.RegisterStudent(username, password, _avatarRelativePath);

                UsernameRegistered = username; // để LoginForm auto fill
                MessageBox.Show("Đăng ký thành công. Bạn có thể đăng nhập.");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // để trống cũng được
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
