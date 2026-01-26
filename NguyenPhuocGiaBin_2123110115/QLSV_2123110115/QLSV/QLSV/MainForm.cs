using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QLSV.BLL;
using QLSV.Models;

namespace QLSV
{
    public partial class MainForm : Form
    {
        private readonly StudentService _studentService = new StudentService();
        private readonly AuthService _authService = new AuthService();

        private int _selectedId = 0;
        private readonly User _currentUser;

        private bool _isLoggingOut = false;

        public MainForm(User u)
        {
            InitializeComponent();
            _currentUser = u;

            // Nếu user bấm X => thoát app
            // Nếu logout => không thoát app (vì quay lại LoginForm)
            this.FormClosed += MainForm_FormClosed;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_isLoggingOut)
                Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Quản lý sinh viên - {_currentUser.Username} ({_currentUser.Role})";

            // Header user + avatar
            ShowCurrentUserHeader();

            // Combo giới tính
            cboGender.Items.Clear();
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");
            cboGender.Items.Add("Khác");
            cboGender.SelectedIndex = 0;

            // DOB nullable
            dtpDob.ShowCheckBox = true;
            dtpDob.Checked = false;

            // Nếu là STUDENT thì không cho CRUD
            ApplyRolePermission();

            // Load danh sách
            LoadStudents();
        }

        private void ApplyRolePermission()
        {
            bool isStudent = string.Equals((_currentUser.Role ?? "").Trim(), "STUDENT", StringComparison.OrdinalIgnoreCase);

            if (isStudent)
            {
                // Không cho thao tác CRUD
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                // Không cho sửa các ô nhập
                txtCode.ReadOnly = true;
                txtName.ReadOnly = true;
                txtPhone.ReadOnly = true;
                txtAddress.ReadOnly = true;
                cboGender.Enabled = false;
                dtpDob.Enabled = false;
                chkActive.Enabled = false;
            }
        }

        private void LoadStudents()
        {
            try
            {
                dgvStudents.AutoGenerateColumns = true;
                dgvStudents.DataSource = _studentService.GetAll();
                dgvStudents.ClearSelection();
                _selectedId = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;

            var item = dgvStudents.CurrentRow.DataBoundItem as Student;
            if (item == null) return;

            _selectedId = item.Id;

            txtCode.Text = item.StudentCode ?? "";
            txtName.Text = item.FullName ?? "";
            txtPhone.Text = item.Phone ?? "";
            txtAddress.Text = item.Address ?? "";

            if (!string.IsNullOrWhiteSpace(item.Gender) && cboGender.Items.Contains(item.Gender))
                cboGender.SelectedItem = item.Gender;
            else
                cboGender.SelectedIndex = 0;

            if (item.Dob.HasValue)
            {
                dtpDob.Checked = true;
                dtpDob.Value = item.Dob.Value;
            }
            else
            {
                dtpDob.Checked = false;
            }

            chkActive.Checked = item.IsActive;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsStudentBlocked()) return;

                var s = ReadForm();
                _studentService.Add(s);

                MessageBox.Show("Thêm sinh viên thành công.");
                LoadStudents();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsStudentBlocked()) return;

                if (_selectedId <= 0)
                {
                    MessageBox.Show("Bạn chưa chọn sinh viên để sửa.");
                    return;
                }

                var s = ReadForm();
                s.Id = _selectedId;
                _studentService.Update(s);

                MessageBox.Show("Cập nhật thành công.");
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsStudentBlocked()) return;

                if (_selectedId <= 0)
                {
                    MessageBox.Show("Bạn chưa chọn sinh viên để xóa.");
                    return;
                }

                var ok = MessageBox.Show("Xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (ok != DialogResult.Yes) return;

                _studentService.Delete(_selectedId);

                MessageBox.Show("Đã xóa.");
                LoadStudents();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvStudents.AutoGenerateColumns = true;
                dgvStudents.DataSource = _studentService.Search(txtSearch.Text);
                dgvStudents.ClearSelection();
                _selectedId = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm: " + ex.Message);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private bool IsStudentBlocked()
        {
            bool isStudent = string.Equals((_currentUser.Role ?? "").Trim(), "STUDENT", StringComparison.OrdinalIgnoreCase);
            if (isStudent)
            {
                MessageBox.Show("Tài khoản sinh viên không có quyền Thêm/Sửa/Xóa.");
                return true;
            }
            return false;
        }

        private Student ReadForm()
        {
            return new Student
            {
                StudentCode = (txtCode.Text ?? "").Trim(),
                FullName = (txtName.Text ?? "").Trim(),
                Dob = dtpDob.Checked ? (DateTime?)dtpDob.Value.Date : null,
                Gender = cboGender.SelectedItem == null ? "" : cboGender.SelectedItem.ToString(),
                Phone = (txtPhone.Text ?? "").Trim(),
                Address = (txtAddress.Text ?? "").Trim(),
                IsActive = chkActive.Checked
            };
        }

        private void ClearForm()
        {
            _selectedId = 0;

            txtCode.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";

            cboGender.SelectedIndex = 0;
            dtpDob.Checked = false;
            chkActive.Checked = true;

            dgvStudents.ClearSelection();
        }

        private void grpStudent_Enter(object sender, EventArgs e)
        {
        }

        // ===== USER HEADER + AVATAR =====

        private void ShowCurrentUserHeader()
        {
            lblUserInfo.Text = $"{_currentUser.Username}\n{_currentUser.Role}";

            // Lấy AvatarPath an toàn (nếu bạn đã có property AvatarPath thì vẫn ok)
            string avatarPath = null;
            try
            {
                var prop = _currentUser.GetType().GetProperty("AvatarPath");
                if (prop != null)
                    avatarPath = prop.GetValue(_currentUser) as string;
            }
            catch
            {
                avatarPath = null;
            }

            try
            {
                // tránh lock file: Dispose ảnh cũ trước
                if (picUserAvatar.Image != null)
                {
                    var old = picUserAvatar.Image;
                    picUserAvatar.Image = null;
                    old.Dispose();
                }

                if (!string.IsNullOrWhiteSpace(avatarPath))
                {
                    string fullPath = Path.Combine(Application.StartupPath, avatarPath);
                    if (File.Exists(fullPath))
                    {
                        picUserAvatar.Image = LoadImageNoLock(fullPath);
                    }
                }
            }
            catch
            {
                picUserAvatar.Image = null;
            }
        }

        private Image LoadImageNoLock(string fullPath)
        {
            byte[] bytes = File.ReadAllBytes(fullPath);
            using (var ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        // NEW: đổi avatar
        private void btnChangeAvatar_Click(object sender, EventArgs e)
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

                    // lưu đường dẫn tương đối vào DB
                    string relativePath = Path.Combine("avatars", fileName);

                    // BẮT BUỘC: AuthService phải có UpdateAvatar(userId, avatarPath)
                    _authService.UpdateAvatar(_currentUser.Id, relativePath);

                    // update user in-memory (nếu có property AvatarPath)
                    try
                    {
                        var prop = _currentUser.GetType().GetProperty("AvatarPath");
                        if (prop != null && prop.CanWrite)
                            prop.SetValue(_currentUser, relativePath);
                    }
                    catch { }

                    // refresh UI
                    ShowCurrentUserHeader();
                    MessageBox.Show("Đổi avatar thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đổi avatar: " + ex.Message);
                }
            }
        }

        // ===== LOGOUT =====
        private void btnLogout_Click(object sender, EventArgs e)
        {
            _isLoggingOut = true;

            // Hiện lại LoginForm đang bị Hide (Application.Run chạy bằng LoginForm)
            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm)
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }

            this.Close();
        }
    }
}
