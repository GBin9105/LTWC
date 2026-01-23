using MySql.Data.MySqlClient;

namespace QLSV.DAL
{
    public static class Db
    {
        private const string ConnStr =
            "Server=127.0.0.1;Port=3306;Database=qlsv;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnStr);
        }
    }
}
