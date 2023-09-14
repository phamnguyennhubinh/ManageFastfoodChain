using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class NhaCungCap
{
    [Display(Name = "Mã nhà cung cấp")]
    public int MaNcc { get; set; }
    [Display(Name = "Tên nhà cung cấp")]
    public string TenNcc { get; set; } = null!;
    [Display(Name = "Số điện thoại")]
    public string? Sdt { get; set; }
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
