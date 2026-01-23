using MySql.Data.MySqlClient;

namespace QLSV.DAL
{
    public class UserRepository
    {
        public (int id, string username, string hash, string role, bool active)? GetByUsername(string username)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                using (var cmd = new MySqlCommand(@"
                    SELECT id, username, password_hash, role, is_active
                    FROM users
                    WHERE username = @u
                    LIMIT 1;", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) return null;

                        return (
                            rd.GetInt32("id"),
                            rd.GetString("username"),
                            rd.GetString("password_hash"),
                            rd.GetString("role"),
                            rd.GetInt32("is_active") == 1
                        );
                    }
                }
            }
        }

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
    }
}
