using System;
using System.Collections.Generic;

namespace FastFoodChain16.Models;

public partial class NhanVien
{
    public int MaNv { get; set; }

    public string HoNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public DateTime? NgaySinh { get; set; }

    public DateTime? NgayVaoLam { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
