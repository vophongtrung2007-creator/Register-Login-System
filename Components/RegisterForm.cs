using Register_Login_System.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;

namespace Register_Login_System
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        public bool exitRequest { get; private set; }
        private bool closeRequest;

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            txtEmail.Focus();

            txtEmail.Text = txtEmail.Text.Trim();
            txtUsername.Text = txtUsername.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();
            txtConfirmPassword.Text = txtConfirmPassword.Text.Trim();

            // KIỂM TRA THÔNG TIN HỢP LỆ (GIỮ NGUYÊN)
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !Regex.IsMatch(txtEmail.Text, @"^[^@\s,]+@[^@\s,]+\.[^@\s,]+$"))
            {
                MessageBox.Show("Mail định dạng sai.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Nhập tên tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Nhập mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Nhập lại mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }

            if (txtPassword.TextLength < 8)
            {
                MessageBox.Show("Mật khẩu phải dài hơn hoặc bằng 8 kí tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // --- KẾT NỐI CSDL VÀ BĂM MẬT KHẨU ---

            btnRegister.Enabled = false;
            btnRegister.Text = "Đang xử lý...";

            try
            {
                var ketQuaBam = MatKhau.Tao(txtPassword.Text);

                bool thanhCong = await DatabaseHelper.TaoNguoiDungMoi(
                    txtUsername.Text,
                    ketQuaBam.Bam,
                    ketQuaBam.Salt,
                    txtEmail.Text
                );

                if (!thanhCong)
                {
                    MessageBox.Show("Username đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                }
                else
                {
                    MessageBox.Show("Đăng kí thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    closeRequest = true;
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRegister.Enabled = true;
                btnRegister.Text = "Đăng ký";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            closeRequest = true;
            this.Close();
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeRequest || e.CloseReason != CloseReason.UserClosing) { return; }

            DialogResult res =
                MessageBox.Show("Bạn có muốn thoát chương trình?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (res == DialogResult.Yes)
            {
                exitRequest = true;
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}