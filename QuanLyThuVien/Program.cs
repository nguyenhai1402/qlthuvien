using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    internal static class Program
    {
        static bool KiemTraKetNoi()
        {
            string SN = "";
            string DN = "";

            try
            {
                // 1. Thử đọc thông tin Server Name (SN) và Database Name (DN) từ tệp
                DataHelper.DocTep("Config.ini", out SN, out DN);

                // 2. Nếu đọc thành công (hoặc giá trị rỗng), thử kết nối
                // Kiểm tra cơ bản để tránh crash nếu tệp trống hoàn toàn
                if (string.IsNullOrEmpty(SN) || string.IsNullOrEmpty(DN))
                {
                    return false; // Dữ liệu cấu hình không đủ, coi như thất bại
                }

                DataHelper dt = new DataHelper(SN, DN);
                dt.MoKetNoi();
                dt.DongKetNoi();

                DataHelper.dt=dt;
                return true; // Kết nối thành công
            }
            
            catch (Exception)
            {
              
                return false;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (KiemTraKetNoi())
            {

                Application.Run(new DangNhap());
            }
            else
            {

                Application.Run(new CauHinhDB());
            }
        }
    }
}
