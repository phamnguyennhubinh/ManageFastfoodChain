using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class SanPham
{
    [Display(Name = "Mã sản phẩm")]
    public int MaSp { get; set; }
    [Display(Name = "Mã loại")]
    public int? MaLoai { get; set; }
    [Display(Name = "Mã nhà cung cấp")]
    public int? MaNcc { get; set; }
    [Display(Name = "Tên sản phẩm")]
    public string TenSp { get; set; } = null!;
    [Display(Name = "Đơn giá")]
    public decimal? DonGia { get; set; }
    [Display(Name = "Mô tả")]
    public string? MoTa { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual LoaiSp? MaLoaiNavigation { get; set; }

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
