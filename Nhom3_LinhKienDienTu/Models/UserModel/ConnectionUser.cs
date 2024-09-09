using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace Nhom3_LinhKienDienTu.Models.UserModel
{
    public class ConnectionUser
    {
        string consql = ConfigurationManager.ConnectionStrings["QL_LinhKienDienTuConnectionString"].ConnectionString;

        public int login(string sdt, string pass)
        {
            SqlConnection con = new SqlConnection(consql);
            string sql = "SELECT COUNT(*) FROM KHACHHANG WHERE SDT ='" + sdt + "' AND MATKHAU = '" + pass + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kt = (int)cmd.ExecuteScalar();
            if (kt > 0)
            {
                return kt;
            }
            con.Close();
            return 0;
        }
        public string TimMaKHLogin(string sdt, string pass)
        {
            SqlConnection con = new SqlConnection(consql);
            string sql = "SELECT MAKH FROM KHACHHANG WHERE SDT ='" + sdt + "' AND MATKHAU = '" + pass + "' ";

            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            string maKH = (string)cmd.ExecuteScalar();
            con.Close();

            return maKH;
        }

        public int Register(string sdt, string pass)
        {
            SqlConnection con = new SqlConnection(consql);
            string sql = "SELECT COUNT(*) FROM KHACHHANG WHERE SDT ='" + sdt + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kt = (int)cmd.ExecuteScalar();
            int rs = 0;
            string LayMaTX = "SELECT MAKH FROM KHACHHANG ORDER BY MAKH DESC";
            SqlDataAdapter da = new SqlDataAdapter(LayMaTX, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string s = dt.Rows[0]["MAKH"].ToString();
            int num = int.Parse(s.Substring(2)) + 1;
            string MaKH = "KH0" + num.ToString();
            if (kt == 0)
            {
                string sql1 = "INSERT INTO KHACHHANG(MAKH,TENKH,GIOITINH,SDT,MATKHAU) VALUES ('" + MaKH + "','User',N'Nam', '" + sdt + "', '" + pass + "') ";
                SqlCommand cmd2 = new SqlCommand(sql1, con);
                rs = (int)cmd2.ExecuteNonQuery();
            }
            con.Close();
            return rs;
        }
    }
}