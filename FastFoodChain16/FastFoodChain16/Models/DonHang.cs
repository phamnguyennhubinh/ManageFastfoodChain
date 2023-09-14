using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models;

public partial class DonHang
{
    [Display(Name = "Mã đơn hàng")]
    public int MaDh { get; set; }
    [Display(Name = "Mã khách hàng")]
    public int? MaKh { get; set; }
    [Display(Name = "Mã nhân viên")]
    public int? MaNv { get; set; }
    [Display(Name = "Ghi chú")]
    public string? Omessage { get; set; }
    [Display(Name = "Ngày đặt")]
    public DateTime? NgayDat { get; set; }
    [Display(Name = "Ngày giao")]
    public DateTime? NgayGiao { get; set; }
    [Display(Name = "Địa chỉ nhận")]
    public string? DiaChiNhan { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
