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
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtMa.Text) ||
                    string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                    string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                    string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(cmbQuyen.Text))

            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lblxacnhanmk.Visible == true)
            {
                MessageBox.Show("Mật khẩu nhập lại chưa chính xác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra và lấy đối tượng DataHelper
            if (DataHelper.dt == null)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu chưa được thiết lập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int quyen = cmbQuyen.SelectedItem.ToString()=="Admin"?0:1;//neeus admin thì 0 nhân viên thì 1
            string caulenhsql = "insert into taikhoan values ('"+txtMa.Text+"','"+ txtTaiKhoan.Text + "','"+txtMatKhau.Text + "',N'"+txtHoTen.Text + "','"+txtSoDienThoai.Text+"',N'"+txtDiaChi.Text +"',"+quyen+")";


            try
            {
                
                //Tạo SqlCommand bằng DataHelper qua phương thức TaoCommand
                SqlCommand cmd = DataHelper.dt.TaoCommand(caulenhsql);
                // Mở kết nối
                DataHelper.dt.MoKetNoi();

                //  Thực thi và lấy số lượng bản ghi
                int count = (int)cmd.ExecuteNonQuery();
                // Đóng kết nối
                DataHelper.dt.DongKetNoi();
                if (count > 0)
                {
                    MessageBox.Show("Đăng ký thành công, bạn sẽ được chuyển hướng đến trang đăng nhập");
                   
                   
                    this.Close();
                }
               
            }
            catch (SqlException ex)
            {
                // Mã lỗi trùng khóa chính = 2627
                // Lỗi trùng UNIQUE = 2601
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Mã người dùng hoặc tài khoản đã tồn tại!");
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
            }
        }

        

        private void txtNhapLaiMK_TextChanged(object sender, EventArgs e)
        {
            if (txtNhapLaiMK.Text == "")
            {
                lblxacnhanmk.Visible = false;
                return;
            }

            if (txtNhapLaiMK.Text != txtMatKhau.Text)
            {
                lblxacnhanmk.Visible = true;
            }
            else
            {
                lblxacnhanmk.Visible = false;
            }
            
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            lblxacnhanmk.Visible = false;
        }
    }
}
