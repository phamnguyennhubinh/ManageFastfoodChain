using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class TaiKhoan
{
    [Display(Name = "Mã tài khoản")]
    public int MaTk { get; set; }
    [Display(Name = "Tên đăng nhập")]

    public string TenDangNhap { get; set; } = null!;
    [Display(Name = "Mật khẩu")]
    public string MatKhau { get; set; } = null!;

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
