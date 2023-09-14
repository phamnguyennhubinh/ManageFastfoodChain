using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class ChiTietDonHang
{
    [Display(Name = "Mã đơn hàng")]
    public int MaDh { get; set; }
    [Display(Name = "Mã sản phẩm")]
    public int MaSp { get; set; }
    [Display(Name = "Số lượng")]
    public int SoLuong { get; set; }
    [Display(Name = "Đơn giá")]
    public decimal DonGia { get; set; }
    [Display(Name = "Mã tài khoản")]
    public int? MaTk { get; set; }
    [Display(Name = "Giảm giá")]
    public float Discount { get; set; }

    public virtual DonHang MaDhNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
