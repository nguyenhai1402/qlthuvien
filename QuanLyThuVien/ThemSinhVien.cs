using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace QuanLyThuVien
{
    public partial class ThemSinhVien: Form
    {
        public ThemSinhVien()
        {
            InitializeComponent();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in panel2.Controls)
            {
                if (ctr is TextBox)
                {
                    (ctr as TextBox).Clear();
                }
                else if (ctr is MaskedTextBox)
                {
                    (ctr as MaskedTextBox).Clear();
                }
            }
            txtHoTen.Focus();
            errorProvider1.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "insert into student(hoTen, so_the_dang_ki, Lop, soDienThoai, diaChi, email)" +
                "\r\nvalues (@hoten, @sothe, @lop, @sdt, @dchi, @email)";
            try
            {
                // Lấy dữ liệu dạng sạch (bỏ các kí tự của mask như: - _ );
                txtSoThe.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                txtSDT.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtSoThe.Text) ||
                    string.IsNullOrWhiteSpace(txtLop.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
                cmd.Parameters.AddWithValue("@hoten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@sothe", txtSoThe.Text);
                cmd.Parameters.AddWithValue("@lop", txtLop.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@dchi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                DataHelper.dt.MoKetNoi();
                int tmp = cmd.ExecuteNonQuery();
                DataHelper.dt.DongKetNoi();
                if (tmp > 0)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
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
