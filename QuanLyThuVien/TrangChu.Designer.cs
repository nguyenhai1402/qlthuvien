namespace QuanLyThuVien
{
    partial class TrangChu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrangChu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themSachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemSachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sinhVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themSinhVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemThongTinSinhVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhanVienStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemThôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phieuSachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traSachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thongKeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Wheat;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sachToolStripMenuItem,
            this.sinhVienToolStripMenuItem,
            this.nhanVienStripMenuItem,
            this.phieuSachToolStripMenuItem,
            this.traSachToolStripMenuItem,
            this.thongKeToolStripMenuItem,
            this.thoatToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(893, 58);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sachToolStripMenuItem
            // 
            this.sachToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themSachToolStripMenuItem,
            this.xemSachToolStripMenuItem});
            this.sachToolStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sachToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_Sach;
            this.sachToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sachToolStripMenuItem.Name = "sachToolStripMenuItem";
            this.sachToolStripMenuItem.Size = new System.Drawing.Size(109, 54);
            this.sachToolStripMenuItem.Text = "Sách";
            // 
            // themSachToolStripMenuItem
            // 
            this.themSachToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_ThemSach;
            this.themSachToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.themSachToolStripMenuItem.Name = "themSachToolStripMenuItem";
            this.themSachToolStripMenuItem.Size = new System.Drawing.Size(201, 56);
            this.themSachToolStripMenuItem.Text = "Thêm sách";
            this.themSachToolStripMenuItem.Click += new System.EventHandler(this.themSachToolStripMenuItem_Click);
            // 
            // xemSachToolStripMenuItem
            // 
            this.xemSachToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_TimKiem;
            this.xemSachToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xemSachToolStripMenuItem.Name = "xemSachToolStripMenuItem";
            this.xemSachToolStripMenuItem.Size = new System.Drawing.Size(201, 56);
            this.xemSachToolStripMenuItem.Text = "Xem sách";
            this.xemSachToolStripMenuItem.Click += new System.EventHandler(this.xemSachToolStripMenuItem_Click);
            // 
            // sinhVienToolStripMenuItem
            // 
            this.sinhVienToolStripMenuItem.BackColor = System.Drawing.Color.OldLace;
            this.sinhVienToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themSinhVienToolStripMenuItem,
            this.xemThongTinSinhVienToolStripMenuItem});
            this.sinhVienToolStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sinhVienToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_student;
            this.sinhVienToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sinhVienToolStripMenuItem.Name = "sinhVienToolStripMenuItem";
            this.sinhVienToolStripMenuItem.Size = new System.Drawing.Size(146, 54);
            this.sinhVienToolStripMenuItem.Text = "Sinh Viên";
            // 
            // themSinhVienToolStripMenuItem
            // 
            this.themSinhVienToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_ThemStudent;
            this.themSinhVienToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.themSinhVienToolStripMenuItem.Name = "themSinhVienToolStripMenuItem";
            this.themSinhVienToolStripMenuItem.Size = new System.Drawing.Size(303, 56);
            this.themSinhVienToolStripMenuItem.Text = "Thêm sinh viên";
            this.themSinhVienToolStripMenuItem.Click += new System.EventHandler(this.themSinhVienToolStripMenuItem_Click);
            // 
            // xemThongTinSinhVienToolStripMenuItem
            // 
            this.xemThongTinSinhVienToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_TimKiem;
            this.xemThongTinSinhVienToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xemThongTinSinhVienToolStripMenuItem.Name = "xemThongTinSinhVienToolStripMenuItem";
            this.xemThongTinSinhVienToolStripMenuItem.Size = new System.Drawing.Size(303, 56);
            this.xemThongTinSinhVienToolStripMenuItem.Text = "Xem thông tin sinh viên";
            this.xemThongTinSinhVienToolStripMenuItem.Click += new System.EventHandler(this.xemThongTinSinhVienToolStripMenuItem_Click);
            // 
            // nhanVienStripMenuItem
            // 
            this.nhanVienStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemThôngTinToolStripMenuItem});
            this.nhanVienStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F);
            this.nhanVienStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.nhanvien__1_;
            this.nhanVienStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.nhanVienStripMenuItem.Name = "nhanVienStripMenuItem";
            this.nhanVienStripMenuItem.Size = new System.Drawing.Size(147, 54);
            this.nhanVienStripMenuItem.Text = "Nhân viên";
            // 
            // xemThôngTinToolStripMenuItem
            // 
            this.xemThôngTinToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.Timnhanvien;
            this.xemThôngTinToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xemThôngTinToolStripMenuItem.Name = "xemThôngTinToolStripMenuItem";
            this.xemThôngTinToolStripMenuItem.Size = new System.Drawing.Size(316, 56);
            this.xemThôngTinToolStripMenuItem.Text = "Xem danh sách nhân viên";
            this.xemThôngTinToolStripMenuItem.Click += new System.EventHandler(this.xemThôngTinToolStripMenuItem_Click);
            // 
            // phieuSachToolStripMenuItem
            // 
            this.phieuSachToolStripMenuItem.BackColor = System.Drawing.Color.OldLace;
            this.phieuSachToolStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phieuSachToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_PhieuSach;
            this.phieuSachToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.phieuSachToolStripMenuItem.Name = "phieuSachToolStripMenuItem";
            this.phieuSachToolStripMenuItem.Size = new System.Drawing.Size(157, 54);
            this.phieuSachToolStripMenuItem.Text = "Phiếu Sách";
            this.phieuSachToolStripMenuItem.Click += new System.EventHandler(this.phieuSachToolStripMenuItem_Click);
            // 
            // traSachToolStripMenuItem
            // 
            this.traSachToolStripMenuItem.BackColor = System.Drawing.Color.Wheat;
            this.traSachToolStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traSachToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_TraSach;
            this.traSachToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.traSachToolStripMenuItem.Name = "traSachToolStripMenuItem";
            this.traSachToolStripMenuItem.Size = new System.Drawing.Size(139, 54);
            this.traSachToolStripMenuItem.Text = "Trả Sách";
            this.traSachToolStripMenuItem.Click += new System.EventHandler(this.traSachToolStripMenuItem_Click);
            // 
            // thongKeToolStripMenuItem
            // 
            this.thongKeToolStripMenuItem.BackColor = System.Drawing.Color.OldLace;
            this.thongKeToolStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_Thong_ke_sach;
            this.thongKeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.thongKeToolStripMenuItem.Name = "thongKeToolStripMenuItem";
            this.thongKeToolStripMenuItem.Size = new System.Drawing.Size(144, 54);
            this.thongKeToolStripMenuItem.Text = "Thống Kê";
            // 
            // thoatToolStripMenuItem
            // 
            this.thoatToolStripMenuItem.BackColor = System.Drawing.Color.Wheat;
            this.thoatToolStripMenuItem.Font = new System.Drawing.Font("Clarendon BT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thoatToolStripMenuItem.Image = global::QuanLyThuVien.Properties.Resources.bg_Thoat;
            this.thoatToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.thoatToolStripMenuItem.Name = "thoatToolStripMenuItem";
            this.thoatToolStripMenuItem.Size = new System.Drawing.Size(116, 54);
            this.thoatToolStripMenuItem.Text = "Thoát";
            this.thoatToolStripMenuItem.Click += new System.EventHandler(this.thoatToolStripMenuItem_Click);
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QuanLyThuVien.Properties.Resources.backgroundtrangchu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(893, 433);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TrangChu";
            this.Text = "Trang Chủ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TrangChu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themSachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemSachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sinhVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themSinhVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemThongTinSinhVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phieuSachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traSachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thongKeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhanVienStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemThôngTinToolStripMenuItem;
    }
}