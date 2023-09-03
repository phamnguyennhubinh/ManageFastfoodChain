namespace FastFoodChain16.Models
{
    public class Product
    {
        public int MaSp { get; set; }

        public int? MaLoai { get; set; }

        public int? MaNcc { get; set; }

        public string TenSp { get; set; } = null!;

        public decimal? DonGia { get; set; }

        public string? MoTa { get; set; }
    }
}
