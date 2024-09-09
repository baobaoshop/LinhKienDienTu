using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Web.DynamicData;
using System.Drawing;

namespace Nhom3_LinhKienDienTu.Models
{

    public class MyGioHangConnection
    {
        string consql = ConfigurationManager.ConnectionStrings["QL_LinhKienDienTuConnectionString"].ConnectionString;
        dbLinhKienDienTuDataContext db = new dbLinhKienDienTuDataContext();
        public bool ThemGioHang(string MA_KH, string MA_LK)
        {
            if (String.IsNullOrEmpty(MA_KH) || String.IsNullOrEmpty(MA_LK))
            {
                return false;
            }
            LINHKIEN ktLK = db.LINHKIENs.Single(s=>s.MALK==MA_LK);
            int slt = int.Parse((ktLK.SOLUONGTON).ToString());
            if(slt<=0) return true; ;
            SqlConnection con = new SqlConnection(consql);
            int sl = 1;
            string sql;
            IEnumerable<GIOHANG> lstGIOtheoKH = db.GIOHANGs.Where(s => s.MAKH == MA_KH);
            if (lstGIOtheoKH.Count() > 0)
            {

                GIOHANG LKcuaKH = lstGIOtheoKH.SingleOrDefault(s => s.MALK == MA_LK);
                if (LKcuaKH != null)
                {
                    try
                    {
                        sl = int.Parse(LKcuaKH.SOLUONG.ToString());
                    }
                    catch
                    {
                        sl = 0;
                    }
                    sl++;
                    LINHKIEN lkKHO = db.LINHKIENs.SingleOrDefault(s => s.MALK == LKcuaKH.MALK);
                    int soluongtrongkho = int.Parse((lkKHO.SOLUONGTON).ToString());
                    if(soluongtrongkho<sl) sl=soluongtrongkho;
                    sql = "update GIOHANG set SOLUONG = " + sl + " where MAKH = '" + MA_KH + "' and MALK = '" + MA_LK + "'";
                }
                else
                {
                    sql = "insert into GIOHANG values('" + MA_KH + "', '" + MA_LK + "', " + sl + ")";
                }

            } else
            {
                sql = "insert into GIOHANG values('" + MA_KH + "', '" + MA_LK + "', " + sl + ")";
            }
            try
            {
                con.Open();
                
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaGioHang(string MA_KH, string MA_LK)
        {
            SqlConnection con = new SqlConnection(consql);
            if (String.IsNullOrEmpty(MA_KH) || String.IsNullOrEmpty(MA_LK))
            {
                return false;
            }else
            {
                string sql = "delete GIOHANG where MAKH = '" + MA_KH + "' and MALK = '" + MA_LK + "'";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                } catch
                {
                    return false;
                }

            }
        }
        public bool TangGioHang(string MA_KH, string MA_LK)
        {
            SqlConnection con = new SqlConnection(consql);
            if (String.IsNullOrEmpty(MA_KH) || String.IsNullOrEmpty(MA_LK))
            {
                return false;
            }
            else
            {
                IEnumerable<GIOHANG> GIOcuaKH = db.GIOHANGs.Where(s => s.MAKH == MA_KH);
                GIOHANG LKcuaKH = GIOcuaKH.SingleOrDefault(s => s.MALK == MA_LK);
                int sl;
                try
                {
                    sl = int.Parse(LKcuaKH.SOLUONG.ToString());
                }
                catch
                {
                    sl = 0;
                }
                sl++;
                int slt;
                try
                {
                    slt = int.Parse(LKcuaKH.LINHKIEN.SOLUONGTON.ToString());
                }
                catch
                {
                    slt = 0;
                }
                if (sl > slt) sl = slt;
                string sql = "update GIOHANG set SOLUONG = "+sl+" where MAKH = '" + MA_KH + "' and MALK = '" + MA_LK + "'";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
        public bool GiamGioHang(string MA_KH, string MA_LK)
        {
            SqlConnection con = new SqlConnection(consql);
            if (String.IsNullOrEmpty(MA_KH) || String.IsNullOrEmpty(MA_LK))
            {
                return false;
            }
            else
            {
                IEnumerable<GIOHANG> GIOcuaKH = db.GIOHANGs.Where(s => s.MAKH == MA_KH);
                GIOHANG LKcuaKH = GIOcuaKH.SingleOrDefault(s => s.MALK == MA_LK);
                int sl;
                try
                {
                    sl = int.Parse(LKcuaKH.SOLUONG.ToString());
                }
                catch
                {
                    sl = 0;
                }
                sl--;
                if (sl < 1) sl = 1;
                string sql = "update GIOHANG set SOLUONG = " + sl + " where MAKH = '" + MA_KH + "' and MALK = '" + MA_LK + "'";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
        public bool ThanhToan(string MAKH, List<string> lstLK, List<int> lstSL)
        {
            bool success = true;
            SqlConnection con = new SqlConnection(consql);
            if (String.IsNullOrEmpty(MAKH))
            {
                return false;
            }
            else
            {
                string sql ="";

                sql = "declare @MADH char(5) set @MADH = (select top 1 MADH from DONHANG order by MADH DESC) set @MADH = SUBSTRING(@MADH, 3, 3) declare @SODH int = Convert(int, @MADH) set @SODH = @SODH+1 set @MADH = CONCAT('DH', convert(char(3), @SODH)) insert into DONHANG(MADH, MAKH) values (@MADH, '" + MAKH + "') ";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    success = false;
                }
                int i = 0;
                string sql2 = "";
                foreach(string lk in lstLK)
                {
                    sql2 = "declare @MADH char(5) set @MADH = (select top 1 MADH from DONHANG order by MADH DESC) declare @thanhtien float = " + lstSL[i] +" * (select DONGIA from LINHKIEN where MALK = '" + lk+"') insert into CT_DONHANG values (@MADH, '"+lk+"', " + lstSL[i] +", @thanhtien) update LINHKIEN set SOLUONGTON = SOLUONGTON-" + lstSL[i] +" where MALK = '"+lk+"'";
                    i++;
                    try
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand(sql2, con);
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {
                        success = false;
                    }
                }
                string sql3 = "exec CapNhatThanhTien exec CapNhatThanhToan";
                try
                {
                    con.Open();
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    success = false;
                }
                string sql4 = " delete GIOHANG where MAKH = '" + MAKH +"'";
                    try
                {
                    con.Open();
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    success = false;
                }

            }
            return success;
        }
        public bool DaNhanHang(string MADH)
        {
            SqlConnection con = new SqlConnection(consql);
            string sql = "update DONHANG set TRANGTHAI = N'Đã giao xong' where MADH = '" + MADH + "'";
            try
            {
                con.Open();
                SqlCommand cmd4 = new SqlCommand(sql, con);
                cmd4.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}