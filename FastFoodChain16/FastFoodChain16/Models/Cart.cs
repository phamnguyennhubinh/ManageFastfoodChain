using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models
{
    public class Cart
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int MaSp { get; set; }
        [Display(Name = "Mã loại")]
        public int MaLoai { get; set; }
        [Display(Name = "Mã nhà cung cấp")]
        public int MaNcc { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string TenSp { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal? DonGia { get; set; }
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        [Display(Name = "Tổng")]
        public decimal? Total { get { return DonGia * SoLuong; } }
    }
}
