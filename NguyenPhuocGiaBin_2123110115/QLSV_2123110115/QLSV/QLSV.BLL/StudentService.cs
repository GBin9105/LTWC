using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QLSV.DAL;
using QLSV.Models;

namespace QLSV.BLL
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new StudentRepository();

        public List<Student> GetAll() => _repo.GetAll();
        public List<Student> Search(string keyword) => _repo.Search(keyword);

        public void Add(Student s)
        {
            ValidateStudent(s);
            _repo.Insert(s);
        }

        public void Update(Student s)
        {
            if (s.Id <= 0) throw new Exception("ID không hợp lệ.");
            ValidateStudent(s);
            _repo.Update(s);
        }

        public void Delete(int id) => _repo.Delete(id);

        private void ValidateStudent(Student s)
        {
            if (s == null) throw new Exception("Dữ liệu sinh viên rỗng.");

            s.StudentCode = (s.StudentCode ?? "").Trim();
            s.FullName = (s.FullName ?? "").Trim();
            s.Phone = (s.Phone ?? "").Trim();

            if (string.IsNullOrWhiteSpace(s.StudentCode))
                throw new Exception("Mã SV không được để trống.");

            if (string.IsNullOrWhiteSpace(s.FullName))
                throw new Exception("Họ tên không được để trống.");

            // SĐT: đúng 10 số và bắt đầu bằng 0
            if (!Regex.IsMatch(s.Phone, @"^0\d{9}$"))
                throw new Exception("SĐT phải gồm đúng 10 chữ số và bắt đầu bằng 0.");
        }
    }
}
