using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class KhachHang
{
    [Display(Name = "Mã khách hàng")]
    public int MaKh { get; set; }
    [Display(Name = "Họ")]
    public string HoKh { get; set; } = null!;
    [Display(Name = "Tên")]
    public string TenKh { get; set; } = null!;
    [Display(Name = "Số điện thoại")]
    public string Sdt { get; set; } = null!;

    public string? Email { get; set; }
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
