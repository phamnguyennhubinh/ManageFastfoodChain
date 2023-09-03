using System;
using System.Collections.Generic;

namespace FastFoodChain16.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public int? MaLoai { get; set; }

    public int? MaNcc { get; set; }

    public string TenSp { get; set; } = null!;

    public decimal? DonGia { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual LoaiSp? MaLoaiNavigation { get; set; }

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
