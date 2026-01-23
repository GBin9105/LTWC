using System;

namespace QLSV.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }   // Mã SV
        public string FullName { get; set; }      // Họ tên
        public DateTime? Dob { get; set; }        // Ngày sinh (nullable)
        public string Gender { get; set; }        // Nam/Nữ/Khác
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
