using System;
using System.Collections.Generic;

namespace FastFoodChain16.Models;

public partial class NhaCungCap
{
    public int MaNcc { get; set; }

    public string TenNcc { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
