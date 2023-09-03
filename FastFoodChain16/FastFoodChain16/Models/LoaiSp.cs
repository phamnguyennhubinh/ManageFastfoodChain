using System;
using System.Collections.Generic;

namespace FastFoodChain16.Models;

public partial class LoaiSp
{
    public int MaLoai { get; set; }

    public string TenLoai { get; set; } = null!;

    public string? Discription { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
