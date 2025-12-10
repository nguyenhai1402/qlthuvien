using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QuanLyThuVien
{
    public partial class CauHinhDB : Form
    {
        public CauHinhDB()
        {
            
            InitializeComponent();
        }

        private void CauHinhDB_Load(object sender, EventArgs e)
        {
            txtTenMay.Focus();
           
            string SN;
            string DN;
          
            DataHelper.DocTep("Config.ini", out SN, out DN);
           
            txtTenMay.Text = SN;
            txtTenCsdl.Text = DN;
        }

        private void btnCauHinh_Click(object sender, EventArgs e)
        {
            string SN = txtTenMay.Text.Trim();
            string DN = txtTenCsdl.Text.Trim();
            

            DataHelper dt_new = new DataHelper(SN, DN);//Kết nối đến severname và tên database
            try
            {

                dt_new.MoKetNoi();
                dt_new.DongKetNoi();


                DataHelper.GhiTep("Config.ini", SN, DN);
                MessageBox.Show("Cấu hình kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                DataHelper.dt = dt_new;//Sau khi dt_new đã kết nối thì gán kết nối đó cho đối tượng dt tĩnh


                this.Hide();
                DangNhap frmDN = new DangNhap();
                frmDN.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kết nối thất bại. Vui lòng kiểm tra lại thông tin. \nChi tiết lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenMay.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn thoát không?", "Thoát", MessageBoxButtons.YesNo) == DialogResult.Yes){
                Application.Exit();
            }
        }
    }
}
