namespace FastFoodChain16.Models
{
    public class Customer
    {
        public int MaKh { get; set; }

        public string HoKh { get; set; } = null!;

        public string TenKh { get; set; } = null!;

        public string? Sdt { get; set; }

        public string? Email { get; set; }

        public string? DiaChi { get; set; }
    }
}
