using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Nhom3_LinhKienDienTu.Models
{
    public class MyGioHang
    {
        dbLinhKienDienTuDataContext db = new dbLinhKienDienTuDataContext();
        public string iMaLK { get; set; }
        public string sTenLK { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //Khoi tao gio hang
        public MyGioHang(string MaLinhKien)
        {
            iMaLK = MaLinhKien;
            LINHKIEN linhkien = db.LINHKIENs.Single(s => s.MALK == iMaLK);
            sTenLK = linhkien.TENLK;
            sAnhBia = linhkien.MALK;
            dDonGia = double.Parse(linhkien.DONGIA.ToString());
            iSoLuong = 1;
        }
        
    }
}