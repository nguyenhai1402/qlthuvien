using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    internal class DataHelper
    {
        public static DataHelper dt;
        string st = "";
        public SqlConnection con = new SqlConnection();

        /// <summary>
        /// Kết nối cơ sở dữ liệu với quyền Windows
        /// </summary>
  
        public DataHelper(string SV, string DN)
        {
            st = @"Data source=" + SV + "; database=" + DN + "; Integrated security=true";
            con = new SqlConnection(st);
        }

        /// <summary>
        /// Mở kết nối
        /// </summary>
        public void MoKetNoi()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        /// <summary>
        /// Đóng kết nối
        /// </summary>
        public void DongKetNoi()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        /// <summary>
        /// Đọc thông tin kết nối từ tệp
        /// </summary>
        public static void DocTep(String tentep, out string SN, out String DN)
        {
            StreamReader dr = new StreamReader(tentep);

           

            if (!dr.EndOfStream)
                SN = dr.ReadLine();
            else
                SN = "";

            if (!dr.EndOfStream)
                DN = dr.ReadLine();
            else
                DN = "";
            dr.Close();
        }

        /// <summary>
        /// Ghi thông tin kết nối vào tệp
        /// </summary>
        public static void GhiTep(String tentep,  string SN, String DN)
        {
            StreamWriter dw = new StreamWriter(tentep);
            dw.WriteLine(SN);
            dw.WriteLine(DN);
            dw.Close();
        }
        // Khai báo phương thức tạo SqlCommand

        //Phải tạo đối tượng Datahelper trước
        public SqlCommand TaoCommand(string sql, CommandType type = CommandType.Text)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = type;
            return cmd;
        }


        public SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MoKetNoi(); 
                SqlCommand cmd = TaoCommand(sql);
                // CommandBehavior.CloseConnection đảm bảo DataReader đóng kết nối 
                // khi nó đóng (khi gọi Close() trên DataReader)
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
               
                throw new Exception("Lỗi thực thi ExecuteReader: " + ex.Message);
            }
        }

        public object ExecuteScalar(string sql)
        {
            try
            {
                MoKetNoi();
                SqlCommand cmd = TaoCommand(sql);
                object ketqua = cmd.ExecuteScalar();
                DongKetNoi();
                return ketqua;
            }
            catch (Exception ex)
            {
                DongKetNoi();
                throw new Exception("Lỗi thực thi ExecuteScalar: " + ex.Message);
            }
        }


        public int ExecuteNonQuery(string sql)
        {
            try
            {
                MoKetNoi();
                SqlCommand cmd = TaoCommand(sql);
                int sodongthuchien = cmd.ExecuteNonQuery();
                DongKetNoi();
                return sodongthuchien;
            }
            catch (Exception ex)
            {
                DongKetNoi();
                throw new Exception("Lỗi thực thi ExecuteNonQuery: " + ex.Message);
            }
        }

    }
}
