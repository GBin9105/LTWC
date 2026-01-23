using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLSV.Models;

namespace QLSV.DAL
{
    public class StudentRepository
    {
        public List<Student> GetAll()
        {
            var list = new List<Student>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(@"
                    SELECT id, student_code, full_name, dob, gender, phone, address, is_active
                    FROM students
                    ORDER BY id DESC;", conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var s = new Student();
                        s.Id = rd.GetInt32("id");
                        s.StudentCode = rd.GetString("student_code");
                        s.FullName = rd.GetString("full_name");

                        int dobIndex = rd.GetOrdinal("dob");
                        s.Dob = rd.IsDBNull(dobIndex) ? (DateTime?)null : rd.GetDateTime(dobIndex);

                        int genderIndex = rd.GetOrdinal("gender");
                        s.Gender = rd.IsDBNull(genderIndex) ? "" : rd.GetString(genderIndex);

                        int phoneIndex = rd.GetOrdinal("phone");
                        s.Phone = rd.IsDBNull(phoneIndex) ? "" : rd.GetString(phoneIndex);

                        int addressIndex = rd.GetOrdinal("address");
                        s.Address = rd.IsDBNull(addressIndex) ? "" : rd.GetString(addressIndex);

                        s.IsActive = rd.GetInt32("is_active") == 1;

                        list.Add(s);
                    }
                }
            }

            return list;
        }

        public List<Student> Search(string keyword)
        {
            keyword = keyword ?? "";
            var list = new List<Student>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(@"
                    SELECT id, student_code, full_name, dob, gender, phone, address, is_active
                    FROM students
                    WHERE student_code LIKE @k OR full_name LIKE @k
                    ORDER BY id DESC;", conn))
                {
                    cmd.Parameters.AddWithValue("@k", "%" + keyword + "%");

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var s = new Student();
                            s.Id = rd.GetInt32("id");
                            s.StudentCode = rd.GetString("student_code");
                            s.FullName = rd.GetString("full_name");

                            int dobIndex = rd.GetOrdinal("dob");
                            s.Dob = rd.IsDBNull(dobIndex) ? (DateTime?)null : rd.GetDateTime(dobIndex);

                            int genderIndex = rd.GetOrdinal("gender");
                            s.Gender = rd.IsDBNull(genderIndex) ? "" : rd.GetString(genderIndex);

                            int phoneIndex = rd.GetOrdinal("phone");
                            s.Phone = rd.IsDBNull(phoneIndex) ? "" : rd.GetString(phoneIndex);

                            int addressIndex = rd.GetOrdinal("address");
                            s.Address = rd.IsDBNull(addressIndex) ? "" : rd.GetString(addressIndex);

                            s.IsActive = rd.GetInt32("is_active") == 1;

                            list.Add(s);
                        }
                    }
                }
            }

            return list;
        }

        public int Insert(Student s)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(@"
                    INSERT INTO students(student_code, full_name, dob, gender, phone, address, is_active)
                    VALUES(@code, @name, @dob, @gender, @phone, @address, @active);", conn))
                {
                    cmd.Parameters.AddWithValue("@code", s.StudentCode);
                    cmd.Parameters.AddWithValue("@name", s.FullName);
                    cmd.Parameters.AddWithValue("@dob", (object)s.Dob ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@gender", (object)s.Gender ?? "");
                    cmd.Parameters.AddWithValue("@phone", (object)s.Phone ?? "");
                    cmd.Parameters.AddWithValue("@address", (object)s.Address ?? "");
                    cmd.Parameters.AddWithValue("@active", s.IsActive ? 1 : 0);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(Student s)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(@"
                    UPDATE students
                    SET student_code=@code, full_name=@name, dob=@dob, gender=@gender, phone=@phone, address=@address, is_active=@active
                    WHERE id=@id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", s.Id);
                    cmd.Parameters.AddWithValue("@code", s.StudentCode);
                    cmd.Parameters.AddWithValue("@name", s.FullName);
                    cmd.Parameters.AddWithValue("@dob", (object)s.Dob ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@gender", (object)s.Gender ?? "");
                    cmd.Parameters.AddWithValue("@phone", (object)s.Phone ?? "");
                    cmd.Parameters.AddWithValue("@address", (object)s.Address ?? "");
                    cmd.Parameters.AddWithValue("@active", s.IsActive ? 1 : 0);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand("DELETE FROM students WHERE id=@id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
