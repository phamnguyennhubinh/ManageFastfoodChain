using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models
{
    public class User
    {
        [Display(Name = "Mã tài khoản")]
        public int UserID { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public string? UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }  

    }
}
