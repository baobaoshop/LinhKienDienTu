using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom3_LinhKienDienTu.Models.UserModel;

namespace Nhom3_LinhKienDienTu.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        //HTTP
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(User a)
        {
            if (ModelState.IsValid)
            {
                if (a.SDT.Length <= 11 && a.SDT != null && a.PassWord.Length <= 20 && a.PassWord != null)
                {
                    string sdt = a.SDT;
                    string pass = a.PassWord;
                    ConnectionUser model = new ConnectionUser();
                    ViewBag.resul = model.Register(a.SDT, a.PassWord);
                    if (ViewBag.resul == 1)
                    {
                        return RedirectToAction("DangNhap", "User");
                    }
                }
            }
            else
            {
                ViewBag.DangKy = "Đăng Ký Không Thành Công";
            }
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(User login)
        {
            Session["KT"] = "0";
            Session["user"] = new User();
            if (ModelState.IsValid)
            {
                string sdt = login.SDT;
                string pass = login.PassWord;

                ConnectionUser model = new ConnectionUser();
                int result = model.login(login.SDT, login.PassWord);

                if (result == 1)
                {
                    // Now retrieve the MAKH from the database using the login credentials
                    string maKH = model.TimMaKHLogin(sdt, pass);

                    Session["KT"] = "1";
                    Session["user"] = new User() { SDT = login.SDT, PassWord = login.PassWord, MaKhH = maKH };
                    return RedirectToAction("TrangChu", "Home");
                }
                else
                {
                    ViewBag.DangNhap = "Đăng Nhập Không Thành Công (Sai Thông Tin)";
                    return View();
                }
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("TrangChu", "Home");
        }
    }
}