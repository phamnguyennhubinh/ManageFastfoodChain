using System;
using System.Collections.Generic;

namespace FastFoodChain16.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string HoKh { get; set; } = null!;

    public string TenKh { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
