using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class TrangChu: Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void themSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemSach them = new ThemSach();
            them.Show();
        }

        private void xemSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemSach xem = new XemSach();
            xem.Show();
        }

        private void themSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemSinhVien sv = new ThemSinhVien();
            sv.Show();
        }

        private void xemThongTinSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemSinhVien xemsv = new XemSinhVien();
            xemsv.Show();
        }

        private void phieuSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhieuMuon pm = new PhieuMuon();
            pm.Show();
        }

        private void traSachToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (DataHelper.Quyen == 1)
            {
                themSachToolStripMenuItem.Enabled= false;
                thongKeToolStripMenuItem.Enabled= false;
            }
        }

        private void xemThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Danhsachnhanvien ds = new Danhsachnhanvien();
                ds.ShowDialog();
        }
    }
}
