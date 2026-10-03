using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Register_Login_System
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = @"Server=.; Database=QUANLYNGUOIDUNG; Trusted_Connection=True; TrustServerCertificate=True;";

        public static async Task<bool> TaoNguoiDungMoi(string tenDangNhap, string matKhauBam, string salt, string email)
        {
            string sql = "INSERT INTO USERS (TENDANGNHAP, MATKHAUBAM, SALT, HOTEN, EMAIL) VALUES (@ten, @bam, @salt, @hoten, @email)";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ten", tenDangNhap);
            cmd.Parameters.AddWithValue("@bam", matKhauBam);
            cmd.Parameters.AddWithValue("@salt", salt);
            cmd.Parameters.AddWithValue("@hoten", tenDangNhap);
            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                await conn.OpenAsync();
                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                return false;
            }
        }

        public static async Task<(string Salt, string MatKhauBam)?> LayThongTinNguoiDung(string tenDangNhap)
        {
            string sql = "SELECT SALT, MATKHAUBAM FROM USERS WHERE TENDANGNHAP = @ten";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ten", tenDangNhap);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return (reader.GetString(0), reader.GetString(1));
            }
            return null;
        }
    }
}