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
                IsActive = row.Value.active,
                AvatarPath = row.Value.avatarPath
            };
        }

        public void CreateUser(string username, string password, string role = "ADMIN", string avatarPath = null)
        {
            username = (username ?? "").Trim();
            password = password ?? "";
            role = (role ?? "USER").Trim();

            if (username.Length == 0) throw new Exception("Username không được trống.");
            if (password.Length == 0) throw new Exception("Password không được trống.");
            if (role.Length == 0) role = "USER";

            if (_repo.ExistsUsername(username))
                throw new Exception("Username đã tồn tại.");

            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            if (!string.IsNullOrWhiteSpace(avatarPath))
                _repo.InsertUser(username, hash, role, avatarPath);
            else
                _repo.InsertUser(username, hash, role);
        }

        public void RegisterStudent(string username, string password, string avatarPath = null)
        {
            CreateUser(username, password, "STUDENT", avatarPath);
        }

        // ===== CẦN THÊM: UpdateAvatar =====
        public void UpdateAvatar(int userId, string avatarPath)
        {
            if (userId <= 0) throw new Exception("UserId không hợp lệ.");

            avatarPath = string.IsNullOrWhiteSpace(avatarPath) ? null : avatarPath.Trim();

            _repo.UpdateAvatar(userId, avatarPath);
        }
    }
}
