using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models
{
    public class MonthlySaleResult
    {
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string? TenSP { get; set; }
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }  
    }
}
