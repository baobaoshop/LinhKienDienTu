using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Web;
using Nhom3_LinhKienDienTu.Models;

namespace Nhom3_LinhKienDienTu.Models
{
    
    public class SanPhamAPI
    {
        public string MALK;

        public string TENLK;

        public Nullable<int> SOLUONGTON;

        public string MANSX;

        public string MALOAI;

        public Nullable<int> NAMSX;

        public Nullable<double> DONGIA;
        public SanPhamAPI(LINHKIEN lk)
        {
            MALK = lk.MALK;
            TENLK = lk.TENLK;
            SOLUONGTON = lk.SOLUONGTON;
            MANSX = lk.MANSX;
            MALOAI = lk.MALOAI;
            NAMSX = lk.NAMSX;
            DONGIA = lk.DONGIA;
        }
    }
}