using System;
using QLSV.DAL;
using QLSV.Models;

namespace QLSV.BLL
{
    public class AuthService
    {
        private readonly UserRepository _repo;

        public AuthService()
        {
            _repo = new UserRepository();
        }

        /// <summary>
        /// Đăng nhập: trả về User nếu đúng, null nếu sai.
        /// </summary>
        public User Login(string username, string password)
        {
            username = (username ?? "").Trim();
            password = password ?? "";

            if (username.Length == 0 || password.Length == 0)
                return null;

            var row = _repo.GetByUsername(username);
            if (row == null) return null;
            if (!row.Value.active) return null;

            bool ok = BCrypt.Net.BCrypt.Verify(password, row.Value.hash);
            if (!ok) return null;

            return new User
            {
                Id = row.Value.id,
                Username = row.Value.username,
                Role = row.Value.role,
                IsActive = row.Value.active
            };
        }

        /// <summary>
        /// Tạo user mới (hash mật khẩu rồi insert).
        /// Lưu ý: username là UNIQUE trong DB, trùng sẽ throw exception.
        /// </summary>
        public void CreateUser(string username, string password, string role = "ADMIN")
        {
            username = (username ?? "").Trim();
            password = password ?? "";
            role = (role ?? "USER").Trim();

            if (username.Length == 0) throw new Exception("Username không được trống.");
            if (password.Length == 0) throw new Exception("Password không được trống.");
            if (role.Length == 0) role = "USER";

            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            // Có thể kiểm tra tồn tại trước, nhưng để đơn giản cứ insert.
            // Nếu trùng username, DB sẽ báo lỗi duplicate key.
            _repo.InsertUser(username, hash, role);
        }
    }
}
