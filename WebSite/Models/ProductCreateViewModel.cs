namespace WebSite.Models
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? DescountedPrice { get; set; }
        public int Amount { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public List<ProductSizeInfo> SizesInfo { get; set; } // Список інформації про розмір
    }
    public class ProductSizeInfo
    {
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}
