using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Register_Login_System
{
    public static class DatabaseHelper
    {
        // Khai báo chuẩn chuỗi kết nối sử dụng chung cho toàn bộ class
        private const string ConnectionString = @"Server=.; Database=QUANLYNGUOIDUNG; Trusted_Connection=True; TrustServerCertificate=True;";

        public static async Task<(string HoTen, string TenDangNhap, string Email, string NgayTao)?> LayThongTinChiTiet(string tenDangNhap)
        {
            const string sql = "SELECT HoTen, TenDangNhap, Email, NgayTao FROM USERS WHERE TenDangNhap = @ten";

            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 50).Value = tenDangNhap;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return (
                    HoTen: reader["HoTen"]?.ToString() ?? "",
                    TenDangNhap: reader["TenDangNhap"]?.ToString() ?? "",
                    Email: reader["Email"]?.ToString() ?? "",
                    NgayTao: reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]).ToString("dd/MM/yyyy HH:mm") : ""
                );
            }
            return null;
        }

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