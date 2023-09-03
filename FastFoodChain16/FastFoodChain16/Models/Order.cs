namespace FastFoodChain16.Models
{
    public class Order
    {
        public int MaDH { get; set; }
        public int MaKH { get; set; }
        public int MaNV { get; set; }
        public string? OMessage { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public string? DiaChiNhan { get; set; }
    }
}
