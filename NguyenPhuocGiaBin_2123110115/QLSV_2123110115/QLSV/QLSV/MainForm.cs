using System;
using System.Windows.Forms;
using QLSV.BLL;
using QLSV.Models;

namespace QLSV
{
    public partial class MainForm : Form
    {
        private readonly StudentService _studentService = new StudentService();
        private int _selectedId = 0;
        private readonly User _currentUser;

        public MainForm(User u)
        {
            InitializeComponent();
            _currentUser = u;

            // Đảm bảo đóng form là thoát hẳn app (tránh lock DLL khi rebuild)
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // (Tuỳ chọn) Hiển thị user đăng nhập trên title
            this.Text = $"Quản lý sinh viên - {_currentUser.Username} ({_currentUser.Role})";

            // Combo giới tính
            cboGender.Items.Clear();
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");
            cboGender.Items.Add("Khác");
            cboGender.SelectedIndex = 0;

            // DOB nullable
            dtpDob.ShowCheckBox = true;
            dtpDob.Checked = false;

            // Load danh sách
            LoadStudents();

            // QUAN TRỌNG:
            // Nếu bạn đã gắn event SelectionChanged trong Designer rồi, KHÔNG gắn lại ở đây.
            // Nếu Designer chưa gắn, bạn có thể mở dòng dưới:
            // dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
        }

        private void LoadStudents()
        {
            try
            {
                dgvStudents.AutoGenerateColumns = true; // nhanh nhất
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
    }
}
