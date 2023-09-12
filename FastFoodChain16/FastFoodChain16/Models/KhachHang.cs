using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class KhachHang
{
    
    public int MaKh { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ khách hàng.")]
    [Display(Name = "Họ")]
    public string HoKh { get; set; } = null!;
    [Required(ErrorMessage = "Vui lòng nhập tên khách hàng.")]
    [Display(Name = "Tên")]
    public string TenKh { get; set; } = null!;
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại khách hàng.")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    [Display(Name = "Số điện thoại")]
    public string? Sdt { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập Email khách hàng.")]
    [Display(Name = "Email")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ khách hàng.")]
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
