using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked)
                txtMatKhau.PasswordChar = '\0';    // Hiện mật khẩu
            else
                txtMatKhau.PasswordChar = '*';     // Ẩn mật khẩu
        }

        

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra và lấy đối tượng DataHelper
            if (DataHelper.dt == null)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu chưa được thiết lập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string caulenhsql = "SELECT COUNT(*) FROM TaiKhoan WHERE taikhoan = @tk AND matkhau = @mk";

            try
            {
                //Tạo SqlCommand bằng DataHelper qua phương thức TaoCommand
                SqlCommand cmd = DataHelper.dt.TaoCommand(caulenhsql);

                //Thêm tham số 
                cmd.Parameters.AddWithValue("@tk", txtTaiKhoan.Text.Trim());
                cmd.Parameters.AddWithValue("@mk", txtMatKhau.Text.Trim());

                // Mở kết nối
                DataHelper.dt.MoKetNoi();



                //  Thực thi và lấy số lượng bản ghi
                int count = (int)cmd.ExecuteScalar();
                // Đóng kết nối
                DataHelper.dt.DongKetNoi();

                if (count > 0)
                {
                    // Đăng nhập thành công
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    // Vào form dashboard
                    
                }
                else
                {
                    // Đăng nhập thất bại
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu. Vui lòng nhập lại!", "Sai thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi(); // Đảm bảo đóng kết nối nếu có lỗi
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
