namespace Register_Login_System
{
    partial class MainApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblXinChao = new Label();
            lblUsername = new Label();
            lblEmail = new Label();
            lblNgayTao = new Label();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblXinChao
            // 
            lblXinChao.Font = new Font("Segoe UI Emoji", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblXinChao.Location = new Point(39, 32);
            lblXinChao.Name = "lblXinChao";
            lblXinChao.Size = new Size(467, 58);
            lblXinChao.TabIndex = 0;
            lblXinChao.Click += label1_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(39, 107);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(110, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập:";
            lblUsername.Click += label2_Click;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(39, 178);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email:";
            // 
            // lblNgayTao
            // 
            lblNgayTao.AutoSize = true;
            lblNgayTao.Location = new Point(39, 254);
            lblNgayTao.Name = "lblNgayTao";
            lblNgayTao.Size = new Size(172, 20);
            lblNgayTao.TabIndex = 3;
            lblNgayTao.Text = "Lần đăng nhập gần nhất:";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(566, 323);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(121, 47);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // MainApplication
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogout);
            Controls.Add(lblNgayTao);
            Controls.Add(lblEmail);
            Controls.Add(lblUsername);
            Controls.Add(lblXinChao);
            Name = "MainApplication";
            Load += MainApplication_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblXinChao;
        private Label lblUsername;
        private Label lblEmail;
        private Label lblNgayTao;
        private Button btnLogout;
    }
}