using Nhom3_LinhKienDienTu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nhom3_LinhKienDienTu.Controllers.Product
{
    public class ProductAPIController : ApiController
    {
        dbLinhKienDienTuDataContext db = new dbLinhKienDienTuDataContext();
        
        [HttpGet]
        public List<LINHKIEN> GetShowALLProduct()
        {                       
            return db.LINHKIENs.ToList();
        }
        public List<LINHKIEN> GetPartialALLProduct()
        {
            return db.LINHKIENs.Take(20).ToList();
        }

        [HttpGet]
        public List<LOAILK> GetMethodLoaiLK()
        { 
            return db.LOAILKs.ToList(); 
        }

        [HttpGet]
        public List<NHASX> GetMethodNhaSX()
        {
            return db.NHASXes.Take(5).ToList();
        }
    }
}
