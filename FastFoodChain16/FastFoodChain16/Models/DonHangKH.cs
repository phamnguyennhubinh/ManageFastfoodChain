using System.ComponentModel.DataAnnotations;
namespace FastFoodChain16.Models
{
    public class DonHangKH
    {
        [Key]
        [Display(Name = "Mã đơn hàng")]
        public int MaDH { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal DonGia { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public DateTime NgayDat { get; set; }
        [Display(Name = "Ngày giao hàng")]
        public DateTime NgayGiao { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }
        [Display(Name = "Địa chỉ nhận hàng")]
        public string? DiaChiNhan { get; set; }
        [Display(Name = "Ghi chú")]
        public string? Omessage { get; set; }
    }
}
