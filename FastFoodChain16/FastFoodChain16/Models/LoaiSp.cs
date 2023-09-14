using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class LoaiSp
{
    [Display(Name = "Mã loại")]
    public int MaLoai { get; set; }
    [Display(Name = "Tên loại")]
    public string TenLoai { get; set; } = null!;
    [Display(Name = "Ghi chú")]
    public string? Discription { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
