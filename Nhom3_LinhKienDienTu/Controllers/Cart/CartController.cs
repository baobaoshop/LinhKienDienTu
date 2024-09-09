using Nhom3_LinhKienDienTu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom3_LinhKienDienTu.Controllers;
using Nhom3_LinhKienDienTu.Controllers.Product;
using Nhom3_LinhKienDienTu.Models.UserModel;

namespace Nhom3_LinhKienDienTu.Controllers.Cart
{
    public class CartController : Controller
    {
        dbLinhKienDienTuDataContext db = new dbLinhKienDienTuDataContext();
        public ActionResult GioHang()
        {
            ViewBag.Rong = true;
            ViewBag.TongTien = 0;
            if (Session["KT"] != null && (string)Session["KT"] == "1")
            {
                string maKhH = ((User)Session["user"]).MaKhH;
                var lstGioHang = db.GIOHANGs.Where(a => a.MAKH == maKhH).ToList();
                Session["SlGioHang"] = lstGioHang.Count;
                if(lstGioHang.Count > 0)
                {
                    ViewBag.Rong = false;
                    
                    foreach(var item in lstGioHang)
                    {
                        ViewBag.TongTien += Double.Parse((item.LINHKIEN.DONGIA * item.SOLUONG).ToString());
                    }
                }
                return View(lstGioHang);
            }
            else
            {
                return RedirectToAction("DangNhap", "User");
            }

        }
        public ActionResult ThemGioHang(string MALK)
        {
            string MAKH;
            try
            {
                MAKH = ((User)Session["user"]).MaKhH;
            }
            catch
            {
                return View();
            }
            
            MyGioHangConnection m = new MyGioHangConnection();
            LINHKIEN lkkho = db.LINHKIENs.Single(s => s.MALK == MALK);
            int sltkho = int.Parse((lkkho.SOLUONGTON).ToString());
            if (sltkho <= 0) return RedirectToAction("HetHang", "Cart");
            if (m.ThemGioHang(MAKH, MALK))
            {
                return RedirectToAction("GioHang", "Cart");
            }
            else
            {
                return View();
            }
        }
        public ActionResult HetHang()
        {
            return View();
        }
        public ActionResult XoaGioHang(string MALK)
        {
            string MAKH = ((User)Session["user"]).MaKhH;
            MyGioHangConnection m = new MyGioHangConnection();
            if(m.XoaGioHang(MAKH, MALK))
            {
                return RedirectToAction("GioHang", "Cart");
            } else
            {
                return View();
            }
        }
        public ActionResult TangGioHang(string MALK)
        {
            string MAKH = ((User)Session["user"]).MaKhH;
            MyGioHangConnection m = new MyGioHangConnection();
            if (m.TangGioHang(MAKH, MALK))
            {
                return RedirectToAction("GioHang", "Cart");
            }
            else
            {
                return View();
            }
        }
        public ActionResult GiamGioHang(string MALK)
        {
            string MAKH = ((User)Session["user"]).MaKhH;
            MyGioHangConnection m = new MyGioHangConnection();
            if (m.GiamGioHang(MAKH, MALK))
            {
                return RedirectToAction("GioHang", "Cart");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ThanhToan()
        {
            try
            {
                string MAKH = ((User)Session["user"]).MaKhH;
                List<GIOHANG> lst = db.GIOHANGs.Where(s => s.MAKH == MAKH).ToList();
                List<string> listLK = new List<string>();
                List<int> listSL = new List<int>();
                foreach (var item in lst)
                {
                    listSL.Add(int.Parse((item.SOLUONG).ToString()));
                    listLK.Add(item.MALK);
                }
                MyGioHangConnection m = new MyGioHangConnection();
                if (m.ThanhToan(MAKH, listLK, listSL))
                {
                    return RedirectToAction("DonHang", "Cart");
                }
                else
                {
                    return View();
                }
            } catch
            {
                return View();
            }
        }
        public ActionResult DonHang()
        {
            string MAKH = ((User)Session["user"]).MaKhH;
            var lstGioHang = db.GIOHANGs.Where(a => a.MAKH == MAKH).ToList();
            Session["SlGioHang"] = lstGioHang.Count;
            List<DONHANG> lst = db.DONHANGs.Where(s=>s.MAKH==MAKH).ToList();
            List<int> lstSL = new List<int>();
            foreach(var item in lst)
            {
                List<CT_DONHANG> lstCT = db.CT_DONHANGs.Where(s => s.MADH == item.MADH).ToList();
                lstSL.Add(lstCT.Count);
            }
            ViewBag.lstSL = lstSL;
            return View(lst);
        }
        public ActionResult DaNhanHang(string MADH)
        {
            if (String.IsNullOrEmpty(MADH)) return View();
            MyGioHangConnection m = new MyGioHangConnection();
            if (m.DaNhanHang(MADH))
            {
                return RedirectToAction("DonHang", "Cart");
            }
            else
            {
                return View();
            }
        }
        public ActionResult CTDonHang(string MADH)
        {
            if (String.IsNullOrEmpty(MADH)) ViewBag.Loi = "loi";
            List<CT_DONHANG> lst = db.CT_DONHANGs.Where(s => s.MADH == MADH).ToList();
            return View(lst);
        }
    }
}