using System;
using MySql.Data.MySqlClient;

namespace QLSV.DAL
{
    public class UserRepository
    {
        // =======================
        // 1) LẤY USER THEO USERNAME (có avatar_path nếu tồn tại)
        // =======================
        public (int id, string username, string hash, string role, bool active, string avatarPath)? GetByUsername(string username)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Nếu DB chưa có avatar_path, bạn có thể đổi query về bản cũ (không avatar).
                using (var cmd = new MySqlCommand(@"
                    SELECT id, username, password_hash, role, is_active, avatar_path
                    FROM users
                    WHERE username = @u
                    LIMIT 1;", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) return null;

                        int avatarIndex;
                        string avatar = null;

                        // Tránh crash nếu DB chưa có cột avatar_path
                        try
                        {
                            avatarIndex = rd.GetOrdinal("avatar_path");
                            avatar = rd.IsDBNull(avatarIndex) ? null : rd.GetString(avatarIndex);
                        }
                        catch
                        {
                            avatar = null;
                        }

                        return (
                            rd.GetInt32("id"),
                            rd.GetString("username"),
                            rd.GetString("password_hash"),
                            rd.GetString("role"),
                            rd.GetInt32("is_active") == 1,
                            avatar
                        );
                    }
                }
            }
        }

        // =======================
        // 2) CHECK TRÙNG USERNAME
        // =======================
        public bool ExistsUsername(string username)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE username=@u;", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // =======================
        // 3) INSERT USER (bản cũ - vẫn giữ để bạn không bị lỗi code hiện tại)
        // =======================
        public int InsertUser(string username, string passwordHash, string role)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                using (var cmd = new MySqlCommand(@"
                    INSERT INTO users(username, password_hash, role, is_active)
                    VALUES(@u, @p, @r, 1);", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", passwordHash);
                    cmd.Parameters.AddWithValue("@r", role);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // =======================
        // 4) INSERT USER (có avatar_path)
        // =======================
        public int InsertUser(string username, string passwordHash, string role, string avatarPath)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                using (var cmd = new MySqlCommand(@"
                    INSERT INTO users(username, password_hash, role, is_active, avatar_path)
                    VALUES(@u, @p, @r, 1, @a);", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", passwordHash);
                    cmd.Parameters.AddWithValue("@r", role);
                    cmd.Parameters.AddWithValue("@a", (object)avatarPath ?? DBNull.Value);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // =======================
        // 5) UPDATE AVATAR (tuỳ chọn)
        // =======================
        public int UpdateAvatar(int userId, string avatarPath)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                using (var cmd = new MySqlCommand(@"
                    UPDATE users
                    SET avatar_path = @a
                    WHERE id = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters.AddWithValue("@a", (object)avatarPath ?? DBNull.Value);

                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
