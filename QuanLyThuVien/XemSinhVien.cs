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
    public partial class XemSinhVien: Form
    {
        public XemSinhVien()
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string sql = "delete from student where ma_sinh_vien = @id";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@id", id);
                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp > 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                XemSinhVien_Load(sender, e);

                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy giá trị cột 0 mà người dùng chọn
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // truy vấn
            string sql = "update student" +
                "\r\n   set hoTen = @hoten," +
                "\r\n       so_the_dang_ki = @sothe," +
                "\r\n       Lop = @lop," +
                "\r\n       soDienThoai = @sdt," +
                "\r\n       diaChi = @dchi," +
                "\r\n       email = @email" +
                "\r\n       where ma_sinh_vien = @id";
            try
            {
                // kiểm tra số thẻ
                if (txtSoThe.Text.Length != 8)
                {
                    errorProvider1.SetError(txtSoThe, "Số thẻ phải gồm đúng 8 số!");
                    MessageBox.Show("Số thẻ phải gồm đúng 8 số!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }
                // kiểm tra số điện thoại
                if (txtSDT.Text.Length != 10)
                {
                    errorProvider1.SetError(txtSDT, "Số điện thoại phải gồm đúng 10 số!");
                    MessageBox.Show("Số điện thoại phải gồm đúng 10 số!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Kiểm tra email hợp lệ
                string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(txtEmail.Text, patternEmail))
                {
                    errorProvider1.SetError(txtEmail, "Vui lòng nhập đúng định dạng (ví dụ: abc@gmail.com)");
                    MessageBox.Show("Email không hợp lệ! Vui lòng nhập đúng định dạng (ví dụ: abc@gmail.com)",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }
                // truy van
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@hoten", txtHoTen.Text.Trim());
                cmd.Parameters.AddWithValue("@sothe", txtSoThe.Text);
                cmd.Parameters.AddWithValue("@lop", txtLop.Text.Trim());
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@dchi", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp > 0)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                XemSinhVien_Load(sender, e);

                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemSinhVien_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            try
            {
                string sql = "select * from student";
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ptb_load.Visible = false;
            ptb_Tim.Visible = true;
            panel2.Visible = false;
            txtNhapSDK.Clear();
            errorProvider1.Clear();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            panel2.Visible = true;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            txtHoTen.Text = row.Cells[1].Value.ToString();
            txtSoThe.Text = row.Cells[2].Value.ToString();
            txtLop.Text = row.Cells[3].Value.ToString();
            txtSDT.Text = row.Cells[4].Value.ToString();
            txtDiaChi.Text = row.Cells[5].Value.ToString();
            txtEmail.Text = row.Cells[6].Value.ToString();
        }

        private void txtNhapSDK_TextChanged(object sender, EventArgs e)
        {
            ptb_load.Visible = true;
            ptb_Tim.Visible = false;

            if(txtNhapSDK.Text == "")
            {
                ptb_Tim.Visible = true;
                ptb_load.Visible = false;
            }
            // kiểm tra ô txtNhapSDK chi được nhập số
            if (txtNhapSDK.Text == "" || int.TryParse(txtNhapSDK.Text, out _))
            {
                errorProvider1.Clear();
            }
            else if (!(int.TryParse(txtNhapSDK.Text, out _)) || txtSoThe.Text.Length > 8)
            {
                errorProvider1.SetError(txtNhapSDK, "Vui lòng chỉ nhập dạng số và số thẻ phải <= 8");
                return;
            }
            // truy vấn
            string truyVan = "select * from student where so_the_dang_ki like @key";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(truyVan);
                cmd.Parameters.AddWithValue("@key", "%" + txtNhapSDK.Text + "%");
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
    }
}
