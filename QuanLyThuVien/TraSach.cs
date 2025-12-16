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
    public partial class TraSach: Form
    {
        public TraSach()
        {
            InitializeComponent();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            // Truy van
            try
            {
                // kiểm tra số thẻ
                if (txtNhapSoDK.Text.Length != 8)
                {
                    errorProvider1.SetError(txtNhapSoDK, "Số thẻ phải gồm đúng 8 số!");
                    MessageBox.Show("Số thẻ phải gồm đúng 8 số!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }

                DataHelper.dt.MoKetNoi();
                string sql = "select pm.ma_phieu, st.hoTen, st.so_the_dang_ki, s.ma_sach, ds.ten_sach," +
                    "\r\n       pm.ngay_muon, pm.han_tra, s.trang_thai" +
                    "\r\n       from phieu_muon pm" +
                    "\r\n       join student st on st.ma_sinh_vien = pm.ma_sinh_vien" +
                    "\r\n       join sach s on s.ma_sach = pm.ma_sach" +
                    "\r\n       join dausach ds on ds.madausach = s.madausach" +
                    "\r\n       left join phieu_tra pt on pm.ma_phieu = pt.ma_phieu" +
                    "\r\n       where st.so_the_dang_ki = @sothe" +
                    "\r\n       and pt.ma_phieu is null" +
                    "\r\n       and s.trang_thai = N'Đang mượn'";

                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@sothe", txtNhapSoDK.Text);
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
        int maPhieuMuon = -1;
        int maSach = -1;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            panel2.Visible = true;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            maPhieuMuon = Convert.ToInt32(row.Cells[0].Value);
            maSach = Convert.ToInt32(row.Cells[3].Value.ToString());

            txtMaSach.Text = maSach.ToString();
            txtTenSach.Text = row.Cells[4].Value.ToString();

            DateTime ngayMuon = Convert.ToDateTime(row.Cells[5].Value);
            DateTime ngayTra = Convert.ToDateTime(row.Cells[6].Value);
            dtpMuon.Value = ngayMuon;
            dtpTra.Value = ngayTra;

            
        }

        private void TraSach_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtNhapSoDK.Clear();
            dataGridView1.DataSource = null;
            panel2.Visible = false;
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTrangThai.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cboTrangThai, "Vui lòng không bỏ trống!");
                    return;
                }
                errorProvider1.Clear();

                DataHelper.dt.MoKetNoi();

                // 1. lấy hạn trả
                string sqlHanTra = "select han_tra from phieu_muon where ma_phieu = @maphieu";
                SqlCommand cmdHan = DataHelper.dt.TaoCommand(sqlHanTra);
                cmdHan.Parameters.AddWithValue("@maphieu", maPhieuMuon);

                DateTime hanTra = Convert.ToDateTime(cmdHan.ExecuteScalar());
                DateTime ngayTra = DateTime.Today;

                int tienPhat = 0;

                // 2. tính tiền phạt
                if (cboTrangThai.Text == "Đúng hạn")
                {
                    tienPhat = 0;
                }
                else if (cboTrangThai.Text == "Quá hạn")
                {
                    int soNgayTre = (ngayTra - hanTra).Days;
                    tienPhat = soNgayTre * 1000;
                }
                else if (cboTrangThai.Text == "Hỏng" || cboTrangThai.Text == "Mất")
                {
                    string sqlGia = "select ds.don_Gia from sach s join dausach ds on s.madausach = ds.madausach" +
                        "\r\n       where s.ma_sach = @masach";
                    SqlCommand cmdGia = DataHelper.dt.TaoCommand(sqlGia);
                    cmdGia.Parameters.AddWithValue("@masach", maSach);

                    int gia = Convert.ToInt32(cmdGia.ExecuteScalar());

                    tienPhat = cboTrangThai.Text == "Hỏng" ? gia / 2 : gia;
                }

                // 3. insert phiếu trả
                string sqlInsert = "insert into phieu_tra(ma_phieu, ngay_tra, tien_phat)" +
                             "\r\n  values(@maphieu, @ngaytra, @tienphat)";
                SqlCommand cmdInsert = DataHelper.dt.TaoCommand(sqlInsert);
                cmdInsert.Parameters.AddWithValue("@maphieu", maPhieuMuon);
                cmdInsert.Parameters.AddWithValue("@ngaytra", ngayTra);
                cmdInsert.Parameters.AddWithValue("@tienphat", tienPhat);
                cmdInsert.ExecuteNonQuery();

                // 4. cập nhật trạng thái sách
                string trangThaiSach;
                if (cboTrangThai.Text == "Đúng hạn" || cboTrangThai.Text == "Quá hạn")
                {
                    trangThaiSach = "Còn";
                }
                else if (cboTrangThai.Text == "Hỏng")
                {
                    trangThaiSach = "Hỏng";
                }
                else if (cboTrangThai.Text == "Mất")
                {
                    trangThaiSach = "Mất";
                }
                else
                {
                    trangThaiSach = "Còn";
                }


                string sqlSach = "update sach set trang_thai = @tt where ma_sach = @masach";
                SqlCommand cmdSach = DataHelper.dt.TaoCommand(sqlSach);
                cmdSach.Parameters.AddWithValue("@tt", trangThaiSach);
                cmdSach.Parameters.AddWithValue("@masach", maSach);
                cmdSach.ExecuteNonQuery();

                MessageBox.Show($"Trả sách thành công!\nTiền phạt: {tienPhat}đ");


                // load lại sau khi trả xong
                panel2.Visible = false;
                cboTrangThai.SelectedIndex = -1;
                string sql = "select pm.ma_phieu, st.hoTen, st.so_the_dang_ki, s.ma_sach, ds.ten_sach," +
                    "\r\n       pm.ngay_muon, pm.han_tra, s.trang_thai" +
                    "\r\n       from phieu_muon pm" +
                    "\r\n       join student st on st.ma_sinh_vien = pm.ma_sinh_vien" +
                    "\r\n       join sach s on s.ma_sach = pm.ma_sach" +
                    "\r\n       join dausach ds on ds.madausach = s.madausach" +
                    "\r\n       left join phieu_tra pt on pm.ma_phieu = pt.ma_phieu" +
                    "\r\n       where st.so_the_dang_ki = @sothe" +
                    "\r\n       and pt.ma_phieu is null";

                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@sothe", txtNhapSoDK.Text);
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

        private void btnXemPhat_Click(object sender, EventArgs e)
        {
            DataHelper.dt.MoKetNoi();
            panel2.Visible = false;
            string sql = "select pm.ma_phieu, st.hoTen, st.so_the_dang_ki, s.ma_sach, ds.ten_sach," +
                    "\r\n       pm.ngay_muon, pm.han_tra, pt.ngay_tra, pt.tien_phat" +
                    "\r\n       from phieu_muon pm" +
                    "\r\n       join student st on st.ma_sinh_vien = pm.ma_sinh_vien" +
                    "\r\n       join sach s on s.ma_sach = pm.ma_sach" +
                    "\r\n       join dausach ds on ds.madausach = s.madausach" +
                    "\r\n       left join phieu_tra pt on pm.ma_phieu = pt.ma_phieu" +
                    "\r\n       where st.so_the_dang_ki = @sothe" +
                    "\r\n       and pt.tien_phat is not null";

            SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
            cmd.Parameters.AddWithValue("@sothe", txtNhapSoDK.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            DataHelper.dt.DongKetNoi();
        }
    }
}
