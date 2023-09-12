using System.ComponentModel.DataAnnotations;

namespace FastFoodChain16.Models
{
    public class Cart
    {
        [Key]

        public int MaSp { get; set; }

        public int MaLoai { get; set; }

        public int MaNcc { get; set; }

        public string TenSp { get; set; }

        public decimal? DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal? Total { get { return DonGia * SoLuong; } }
    }
}
