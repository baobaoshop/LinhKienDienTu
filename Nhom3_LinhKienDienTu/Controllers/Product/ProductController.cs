using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom3_LinhKienDienTu.Controllers;
using Nhom3_LinhKienDienTu.Models;

namespace Nhom3_LinhKienDienTu.Controllers.Product
{
    public class ProductController : Controller
    {
        // GET: Product
        dbLinhKienDienTuDataContext db = new dbLinhKienDienTuDataContext();
        
        /*Ajax & Jquery*/
        public ActionResult GetDetail(string productid)
        {
            SanPhamAPI sp = new SanPhamAPI(db.LINHKIENs.Where(a => a.MALK == productid).FirstOrDefault());
            return Json(new
            {
                Product = sp
            }, JsonRequestBehavior.AllowGet);
        }
        /*Show mục lục theo nhà sản xuất*/
        public ActionResult PartialNSX()
        {
            ProductAPIController nsx = new ProductAPIController();
            return View(nsx.GetMethodNhaSX());
        }
        /* Show mục lục loại linh kiện*/
        public ActionResult PartialLoaiLK()
        {
            ProductAPIController sp = new ProductAPIController();
            return View(sp.GetMethodLoaiLK());
        }
       
        /* Show sản phẩm theo nhà sản xuất */
        public ActionResult TheoNSX(string id)
        {
            var ListNSX = db.LINHKIENs.Where(a => a.MANSX == id).ToList();
            if (ListNSX.Count() == 0)
            {
                ViewBag.NSX = 0;
            }
            return View(ListNSX);   
        }
        /* Show sản phầm theo loại linh kiện */
        public ActionResult TheoLoaiLK(string id)
        {
            var ListLLK = db.LINHKIENs.Where(a => a.MALOAI == id).ToList();
            if (ListLLK.Count() == 0)
            {
                ViewBag.NSX = 0;
            }
            return View(ListLLK);
        }

        /* Show sản phẩm tất cả */
        
        public ActionResult ShowAllProduct()
        {
            ProductAPIController sp = new ProductAPIController();
            return View(sp.GetShowALLProduct());
        }
        public ActionResult PartialAllProduct()
        {
            ProductAPIController sp = new ProductAPIController();
            return View(sp.GetPartialALLProduct());
        }
        public ActionResult SearchSanPham(string txtSearch)
        {
            
            IEnumerable<LINHKIEN> lst1 = db.LINHKIENs.Where(s => s.TENLK.Contains(txtSearch));
            IEnumerable<LINHKIEN> lst2 = db.LINHKIENs.Where(s => s.NHASX.TENNSX.Contains(txtSearch));
            IEnumerable<LINHKIEN> lst3 = db.LINHKIENs.Where(s => s.LOAILK.TENLOAI.Contains(txtSearch));
            IEnumerable<LINHKIEN>  lst = lst1.Union(lst2).Union(lst3);
            return View(lst);
            
        }
    }
}