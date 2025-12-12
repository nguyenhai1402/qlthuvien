using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class PhieuMuon: Form
    {
        public PhieuMuon()
        {
            InitializeComponent();
        }
        private void PhieuMuon_Load(object sender, EventArgs e)
        {
            loadtensach();
            load_tac_gia();
            // bật gợi ý cho cả 2 cbo
            cboTenSach.AutoCompleteMode = AutoCompleteMode.SuggestAppend; //SuggestAppend: vừa hiện gợi ý vừa tự điền tiếp
            cboTenSach.AutoCompleteSource = AutoCompleteSource.ListItems; //ListItems: Dùng danh sách item có trong ComboBox

            cboTenTG.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTenTG.AutoCompleteSource = AutoCompleteSource.ListItems;

            // ko hiện trước dữ liệu
            cboTenSach.SelectedIndex = -1;
            cboTenTG.SelectedIndex = -1;

            // ko cho datetimePicker chọn ngày hôm qua
            dtpNgayMuon.MinDate = DateTime.Today; // DateTime.Today = 00:00:00 của ngày hiện tại
            dtpNgayTra.MinDate = DateTime.Today;
        }
        // hàm load combobox tên sách
        private void loadtensach()
        {
            DataTable dt = new DataTable();
            try
            {
                DataHelper.dt.MoKetNoi();

                string sql = "select distinct ten_sach from sach";
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                DataHelper.dt.DongKetNoi();

                cboTenSach.DataSource = dt; //gán nguồn dữ liệu cho cbo
                cboTenSach.DisplayMember = "ten_sach"; //chọn cột để hiển thị
                cboTenSach.ValueMember = "ten_sach"; //lấy giá trị
                cboTenSach.SelectedIndex = -1; // ko tự chọn trước dữ liệu
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // hàm load combobox tên tác giả
        private void load_tac_gia()
        {
            DataTable dt = new DataTable();
            try
            {
                DataHelper.dt.MoKetNoi();

                string sql = "select distinct tac_gia from sach";
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                DataHelper.dt.DongKetNoi();

                cboTenTG.DataSource = dt; //gán nguồn dữ liệu cho cbo
                cboTenTG.DisplayMember = "tac_gia"; //chọn cột để hiển thị
                cboTenTG.ValueMember = "tac_gia"; //lấy giá trị
                cboTenTG.SelectedIndex = -1; // ko tự chọn trước dữ liệu
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // khi chọn tên sách -> load tác giả
        private void cboTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenSach.SelectedIndex < 0)
            {
                cboTenTG.Enabled = false;
                return;
            }
            
            DataTable dt = new DataTable();
            cboTenTG.Enabled = true;
            try
            {
                DataHelper.dt.MoKetNoi();

                string sql = "select distinct tac_gia from sach where ten_sach = @ts";
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@ts", cboTenSach.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                DataHelper.dt.DongKetNoi();

                //cboTenTG.Enabled = true;

                cboTenTG.DataSource = dt;
                cboTenTG.DisplayMember = "tac_gia";
                cboTenTG.ValueMember = "tac_gia";
                cboTenTG.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Khi mà có chọn cboTenSach để hiện cboTenTG lên rồi mà lại muốn xóa đoạn text của cboTenSach đấy đi
        // thì cboTenTG lại bị ẩn
        private void cboTenSach_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboTenSach.Text))
            {
                cboTenTG.Enabled = false;
                cboTenTG.Text = ""; 
            }
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
                string sql = "select hoTen, so_the_dang_ki, soDienThoai, diaChi, email " +
                            "\r\nfrom student " +
                            "\r\nwhere so_the_dang_ki = @sothe";
                SqlCommand cmd = DataHelper.dt.TaoCommand(sql);
                cmd.Parameters.AddWithValue("@sothe", txtNhapSoDK.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtHoten.Text = rd.GetString(0);
                    txtSoDK.Text = rd.GetString(1);
                    txtSDT.Text = rd.GetString(2);
                    txtDchi.Text = rd.GetString(3);
                    txtEmail.Text = rd.GetString(4);
                }
                else
                {
                    MessageBox.Show("Bạn chưa đăng kí thông tin Sinh Viên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNhapSoDK.Clear();
                    txtNhapSoDK.Focus();
                }
                    rd.Close();
                DataHelper.dt.DongKetNoi();
            }
            catch(Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            foreach(Control ctr in panel3.Controls)
            {
                if(ctr is TextBox)
                {
                    (ctr as TextBox).Clear();
                    txtTrangThai.Text = "Đang Mượn";
                }
                else if(ctr is MaskedTextBox)
                {
                    (ctr as MaskedTextBox).Clear();
                }
            }
            txtNhapSoDK.Clear();
            cboTenSach.SelectedIndex = -1;
            dtpNgayTra.Value = DateTime.Today;
            txtNhapSoDK.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // kiểm tra nhập
                if (string.IsNullOrWhiteSpace(txtSoDK.Text) ||
                    string.IsNullOrWhiteSpace(cboTenSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataHelper.dt.MoKetNoi();
                // 1. Lấy ma sach để insert vào bảng phieumuon
                string truyVan1 = "select ma_sach from sach where ten_sach = @tensach and tac_gia = @tentg";
                SqlCommand cmd1 = DataHelper.dt.TaoCommand(truyVan1);
                cmd1.Parameters.AddWithValue("@tensach", cboTenSach.Text);
                cmd1.Parameters.AddWithValue("@tentg", cboTenTG.Text);
                object MaSach = cmd1.ExecuteScalar();
                if(MaSach == null)
                {
                    MessageBox.Show("Không tìm thấy sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DataHelper.dt.DongKetNoi();
                    return;
                }
                int maSach = int.Parse(MaSach.ToString());
                // 2. Lấy ma_sinh_vien để insert vào bảng phieumuon
                string truyVan2 = "select ma_sinh_vien from student where so_the_dang_ki = @sothe";
                SqlCommand cmd2 = DataHelper.dt.TaoCommand(truyVan2);
                cmd2.Parameters.AddWithValue("@sothe", txtNhapSoDK.Text);
                object MaSinhVien = cmd2.ExecuteScalar();
                if(MaSinhVien == null)
                {
                    MessageBox.Show("Không tìm thấy sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DataHelper.dt.DongKetNoi();
                    return;
                }
                int masv = int.Parse(MaSinhVien.ToString());

                // 3. Thêm dữ liệu vào bảng phieumuon
                string sql = "insert into phieu_muon(ma_sinh_vien, ma_sach, ngay_muon, han_tra, trang_thai)" +
                            "\r\nvalues(@masv, @masach, @ngaymuon, @ngaytra, @trangthai)";
                SqlCommand sqlcmd = DataHelper.dt.TaoCommand(sql);
                sqlcmd.Parameters.AddWithValue("@masv", masv);
                sqlcmd.Parameters.AddWithValue("@masach", maSach);
                sqlcmd.Parameters.AddWithValue("@ngaymuon", dtpNgayMuon.Value);
                sqlcmd.Parameters.AddWithValue("@ngaytra", dtpNgayTra.Value);
                sqlcmd.Parameters.AddWithValue("@trangthai", txtTrangThai.Text);

                sqlcmd.ExecuteNonQuery();
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Thêm phiếu mượn thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DataHelper.dt.DongKetNoi();
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
