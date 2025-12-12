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
    public partial class XemSach: Form
    {
        public XemSach()
        {
            InitializeComponent();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtNhapSach.Clear();
            panel2.Visible = false;
            errorProvider1.Clear();
            txtNhapTG.Clear();
        }

        private void XemSach_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            try
            {
                string sql = "select * from sach";
                DataHelper.dt.MoKetNoi();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, DataHelper.dt.con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
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

            txtNameBook.Text = row.Cells[1].Value.ToString();
            txtTenTg.Text = row.Cells[2].Value.ToString();
            txtTheLoai.Text = row.Cells[3].Value.ToString();
            txtSoluong.Text = row.Cells[4].Value.ToString();
            txtCon.Text = row.Cells[5].Value.ToString();
        }

        private void txtNhapSach_TextChanged(object sender, EventArgs e)
        {
            string truyVan = "select * from sach where ten_sach like @key";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(truyVan);
                cmd.Parameters.AddWithValue("@key", "%" + txtNhapSach.Text + "%");
                DataHelper.dt.MoKetNoi();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                DataHelper.dt.DongKetNoi();
            }
            catch(Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtNhapTG_TextChanged(object sender, EventArgs e)
        {
            string truyVan = "select * from sach where tac_gia like @key";
            try
            {
                SqlCommand cmd = DataHelper.dt.TaoCommand(truyVan);
                cmd.Parameters.AddWithValue("@key", "%" + txtNhapTG.Text + "%");
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy giá trị cột 0 mà người dùng chọn
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // truy vấn
            string sql = "update sach set ten_sach = @tenSach, " +
                "tac_gia = @tenTG, the_loai = @theLoai, so_luong = @soLuong, so_luong_con = @con where ma_sach = @id";
            try
            {
                if (int.TryParse(txtSoluong.Text, out int value) && value >= 0)
                {
                    errorProvider1.Clear();
                }
                else
                {
                    errorProvider1.SetError(txtSoluong, "Vui lòng nhập số và phải >= 0!");
                    return;
                }
                // kiểm tra số lượng sách (còn) nhập vào phải dương và dạng số
                if (int.TryParse(txtCon.Text, out int res) && res >= 0)
                {
                    errorProvider1.Clear();
                }
                else
                {
                    errorProvider1.SetError(txtCon, "Vui lòng nhập số và phải >= 0!");
                    return;
                }
                // kiểm tra số lượng sách nhập vào có >= số lượng sách còn khi thêm ko
                int tongSL = int.Parse(txtSoluong.Text);
                int soluongTon = int.Parse(txtCon.Text);
                if (tongSL >= soluongTon)
                {
                    errorProvider1.Clear();
                }
                else
                {
                    errorProvider1.SetError(txtCon, "Số lượng sách tồn kho vượt quá số lượng sách nhập!");
                    return;
                }
                // truy van
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@tenSach", txtNameBook.Text.Trim());
                cmd.Parameters.AddWithValue("@tenTG", txtTenTg.Text.Trim());
                cmd.Parameters.AddWithValue("@theLoai", txtTheLoai.Text.Trim());
                cmd.Parameters.AddWithValue("@soLuong", int.Parse(txtSoluong.Text.Trim()));
                cmd.Parameters.AddWithValue("@con", int.Parse(txtSoluong.Text.Trim()));
                cmd.Parameters.AddWithValue("@id", id);

                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                if(tmp > 0)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                XemSach_Load(sender, e);

                DataHelper.dt.DongKetNoi();
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string sql = "delete from sach where ma_sach = @id";
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
                XemSach_Load(sender, e);

                DataHelper.dt.DongKetNoi();
            }
            catch(Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
