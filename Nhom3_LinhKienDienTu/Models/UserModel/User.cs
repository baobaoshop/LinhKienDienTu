using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nhom3_LinhKienDienTu.Models.UserModel
{
    public class User
    {
        private string sDT;
        [Required(ErrorMessage = "Nhập Số Điện Thoại")]
        [StringLength(12)]
        [MinLength(1), MaxLength(12)]
        public string SDT
        {
            get { return sDT; }
            set { sDT = value; }
        }


        private string passWord;
        [Required(ErrorMessage = "Nhập PassWord")]
        [StringLength(20)]
        [MinLength(1), MaxLength(20)]
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
        private string MaKH;
        public string MaKhH
        {
            get { return MaKH; }
            set { MaKH = value; }
        }
        public User()
        {
            SDT = "";
            PassWord = "";
            MaKH = "";
        }

    }

}