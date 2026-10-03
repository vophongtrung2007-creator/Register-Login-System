using System;
using System.Windows.Forms;

namespace Register_Login_System
{
    public partial class MainApplication : Form
    {
        private string currentUsername;

        // Constructor nhận vào tên đăng nhập từ form Login truyền sang
        public MainApplication(string username)
        {
            InitializeComponent();
            currentUsername = username;
        }

        private async void MainApplication_Load(object sender, EventArgs e)
        {
            try
            {
                //Goi ham lay thong tin
                var userInfo = await DatabaseHelper.LayThongTinChiTiet(currentUsername);

                if (userInfo.HasValue)
                {
                    lblXinChao.Text = $"Xin chào, {userInfo.Value.HoTen}";
                    lblUsername.Text = $"Tên đăng nhập: {userInfo.Value.TenDangNhap}";
                    lblEmail.Text = $"Email: {userInfo.Value.Email}";
                    lblNgayTao.Text = $"Lần đăng nhập gần nhất / Ngày tạo:\n{userInfo.Value.NgayTao}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Xác nhận đăng xuất hoặc trực tiếp quay về màn hình đăng nhập
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận (tùy chọn)
            var result = MessageBox.Show("Bạn có muốn đăng xuất khỏi tài khoản?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Ẩn form chính hiện tại
                this.Hide();

                // Mở lại Form đăng nhập
                using (var loginForm = new LoginForm())
                {
                    loginForm.ShowDialog();
                }

                // Đóng hẳn form chính sau khi form đăng nhập đóng lại
                this.Close();
            }
        }
    }
}
