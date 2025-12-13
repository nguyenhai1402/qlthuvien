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
    public partial class ThemSach: Form
    {
        public ThemSach()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "insert into sach(ten_sach, tac_gia, the_loai, so_luong, so_luong_con)" +
                "\r\nvalues(@tensach, @tacgia, @theloai, @soLuong, @soLuongcon)";
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenSach.Text) ||
                    string.IsNullOrWhiteSpace(txtTenTg.Text) ||
                    string.IsNullOrWhiteSpace(txtTheLoai.Text) ||
                    string.IsNullOrWhiteSpace(txtSoluong.Text) ||
                    string.IsNullOrWhiteSpace(txtCon.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // kiểm tra số lượng sách nhập vào phải dương và dạng số
                if(int.TryParse(txtSoluong.Text, out int value) && value >= 0)
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
                if(tongSL >= soluongTon)
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
                cmd.Parameters.AddWithValue("@tensach", txtTenSach.Text);
                cmd.Parameters.AddWithValue("@tacgia", txtTenTg.Text);
                cmd.Parameters.AddWithValue("@theloai", txtTheLoai.Text);
                cmd.Parameters.AddWithValue("@soLuong", int.Parse(txtSoluong.Text));
                cmd.Parameters.AddWithValue("@soLuongcon", int.Parse(txtCon.Text));

                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                DataHelper.dt.DongKetNoi();
                if(tmp > 0)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            foreach(Control ctr in panel2.Controls)
            {
                if(ctr is TextBox)
                {
                    (ctr as TextBox).Clear();
                }
            }
            txtTenSach.Focus();
            errorProvider1.Clear();
        }
    }
}
