using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class DonHang
{
    [Display(Name = "Mã đơn hàng")]
    public int MaDh { get; set; }

    public int? MaKh { get; set; }

    public int? MaNv { get; set; }

    public string? Omessage { get; set; }

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayGiao { get; set; }

    public string? DiaChiNhan { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
