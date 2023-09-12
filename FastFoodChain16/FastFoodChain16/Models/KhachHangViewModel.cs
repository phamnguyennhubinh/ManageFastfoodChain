using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models
{
    public class KhachHangViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ khách hàng.")]
        public string HoKh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng.")]
        public string TenKh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại khách hàng.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email khách hàng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ khách hàng.")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tên đăng nhập.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không được chứa khoảng trắng.")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Mật khẩu không được chứa khoảng trắng.")]
        public string MatKhau { get; set; }

        // Các thuộc tính khác tương ứng với các trường trong form

   

    }

}
