using Register_Login_System.Components;
using System.Text.Json;
using System.Windows.Forms;

namespace Register_Login_System
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            txtUsername.Text = txtUsername.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Nhập tên tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Nhập mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            btnLogin.Enabled = false;
            btnLogin.Text = "Đang xử lý";

            try
            {
                //  Gọi DatabaseHelper để lấy salt và chuỗi băm từ SQL server
                var thongTin = await DatabaseHelper.LayThongTinNguoiDung(txtUsername.Text);

                if (thongTin == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại hoặc sai thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                    return;
                }

                //So sánh
                bool dungMatKhau = MatKhau.KiemTra(txtPassword.Text, thongTin.Value.Salt, thongTin.Value.MatKhauBam);

                if (dungMatKhau)
                {
                    this.Hide();

                    using (var app = new MainApplication())
                    {
                        app.ShowDialog();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu sai.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Đăng nhập";
            }
        }
        private void lklbRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (RegisterForm regForm = new RegisterForm())
            {
                regForm.ShowDialog();

                if (regForm.exitRequest)
                {
                    this.Close();
                }
                else
                {
                    this.Show();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (PasswordRecovery passRec = new PasswordRecovery())
            {
                passRec.ShowDialog();

                if (passRec.exitRequest)
                {
                    this.Close();
                }
                else
                {
                    this.Show();
                }
            }
        }
    }
}
