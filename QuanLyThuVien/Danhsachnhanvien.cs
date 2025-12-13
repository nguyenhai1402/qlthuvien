using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class Danhsachnhanvien : Form
    {
        public Danhsachnhanvien()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            // Lấy giá trị cột 0 mà người dùng chọn
            string manv = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            // truy vấn
            string sql = "update taikhoan" +
                "\r\n   set hoTen = @hoten," +
                "\r\n       SoDienThoai=@sdt," +    
                "\r\n       diaChi = @dchi," +
                "\r\n       quyen =@quyen" +
                "\r\n       where maNguoiDung = @manv";
            try
            {

                if (string.IsNullOrWhiteSpace(txtHoTen.Text)){
                    errorProvider1.SetError(txtHoTen, "Vui lòng nhập họ tên!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
                {
                    errorProvider1.SetError(txtDiaChi, "Vui lòng nhập địa chỉ!");
                    return;
                }

                // kiểm tra số điện thoại
                if (txtSDT.Text.Length != 10)
                {
                    errorProvider1.SetError(txtSDT, "Số điện thoại phải gồm đúng 10 số!");
                    MessageBox.Show("Số điện thoại phải gồm đúng 10 số!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // truy van
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@hoten", txtHoTen.Text.Trim());
                
              
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@dchi", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@quyen", txtQuyen.Text.Trim().ToString()=="Nhân viên"?1:0);
                cmd.Parameters.AddWithValue("@manv", manv);

                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp > 0)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Danhsachnhanvien_Load(sender, e);
                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Danhsachnhanvien_Load(object sender, EventArgs e)
        {
            
            panel2.Visible = false;
            try
            {
                string sql = "select MaNguoiDung as [Mã nhân viên],HoTen as [Họ và tên],SoDienThoai as [Số điện thoại],DiaChi as [Địa chỉ],CASE WHEN quyen = 1 THEN N'Nhân viên' WHEN quyen = 0 THEN N'Admin' END AS [Chức vụ] from taikhoan where quyen=1";
                DataHelper.dt.MoKetNoi();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, DataHelper.dt.con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {

                lbltim.Visible = true;
                lbldangtim.Visible = false;
                ptcdangtim.Visible = false;
                ptctim.Visible = true;
            }
            else
            {
                lbltim.Visible = false;
                lbldangtim.Visible = true;
                ptcdangtim.Visible = true;
                ptctim.Visible = false;
            }
            string truyVan = "select MaNguoiDung as [Mã nhân viên],HoTen as [Họ và tên],SoDienThoai as [Số điện thoại],DiaChi as [Địa chỉ],CASE WHEN quyen = 1 THEN N'Nhân viên' WHEN quyen = 0 THEN N'Admin' END AS [Chức vụ] from taikhoan where quyen=1 and manguoidung like @key";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(truyVan);
                cmd.Parameters.AddWithValue("@key", "%" + txtMaNhanVien.Text + "%");
                DataHelper.dt.MoKetNoi();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtTen_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {

                lbltim.Visible = true;
                lbldangtim.Visible = false;
                ptcdangtim.Visible = false;
                ptctim.Visible = true;
            }
            else
            {
                lbltim.Visible = false;
                lbldangtim.Visible = true;
                ptcdangtim.Visible = true;
                ptctim.Visible = false;
            }
            string truyVan = "select MaNguoiDung as [Mã nhân viên],HoTen as [Họ và tên],SoDienThoai as [Số điện thoại],DiaChi as [Địa chỉ],CASE WHEN quyen = 1 THEN N'Nhân viên' WHEN quyen = 0 THEN N'Admin' END AS [Chức vụ] from taikhoan where quyen=1 and hoten like @key";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(truyVan);
                cmd.Parameters.AddWithValue("@key", "%" + txtTen.Text + "%");
                DataHelper.dt.MoKetNoi();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            panel2.Visible = true;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            txtHoTen.Text = row.Cells[1].Value.ToString();
            
            
            txtSDT.Text = row.Cells[2].Value.ToString();
            txtDiaChi.Text = row.Cells[3].Value.ToString();
            txtQuyen.Text=row.Cells[4].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            // Lấy giá trị cột 0 mà người dùng chọn
            string manv = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            string sql = "delete from taikhoan where manguoidung = @manv";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@manv", manv);
                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp > 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Danhsachnhanvien_Load(sender, e);

                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaNhanVien_Enter(object sender, EventArgs e)
        {
            txtTen.Clear();
            txtTen.ReadOnly = true;

            txtMaNhanVien.ReadOnly = false;
        }

        private void txtTen_Enter(object sender, EventArgs e)
        {
            txtMaNhanVien.Clear();
            txtMaNhanVien.ReadOnly = true;

            txtTen.ReadOnly = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTen.Clear();
            panel2.Visible = false;
            errorProvider1.Clear();
            txtMaNhanVien.Clear();
        }
    }
}
