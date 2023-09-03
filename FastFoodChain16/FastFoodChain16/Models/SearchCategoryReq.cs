namespace FastFoodChain16.Models
{
    public class SearchCategoryReq
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Keyword { get; set; }
    }
}
