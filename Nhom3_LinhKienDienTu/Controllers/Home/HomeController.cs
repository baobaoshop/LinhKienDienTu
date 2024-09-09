using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom3_LinhKienDienTu.Models;
using Nhom3_LinhKienDienTu.Models.UserModel;

namespace Nhom3_LinhKienDienTu.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        dbLinhKienDienTuDataContext db = new dbLinhKienDienTuDataContext();
        public ActionResult TrangChu()
        {
            try
            {
                string maKhH = ((User)Session["user"]).MaKhH;
                var lstGioHang = db.GIOHANGs.Where(a => a.MAKH == maKhH).ToList();
                Session["SlGioHang"] = lstGioHang.Count;
            }
            catch
            {
                Session["SlGioHang"] = 0;
            }
            
            
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult TinTuc()
        {
            return View();
        }
        public ActionResult ThongTinSinhVien()
        {
            return View();
        }
    }
}