using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FastFoodChain16.Models;

public partial class TaiKhoan
{
    [Display(Name = "STT")]
    public int MaTk { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
    [Display(Name = "Tên đăng nhập")]
    public string TenDangNhap { get; set; } = null!;
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [Display(Name = "Mật khẩu")]
    public string MatKhau { get; set; } = null!;

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
