using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }     
        public double Price { get; set; }
        public double? DiscountedPrice { get; set; }

        public string Photo {  get; set; }
    
        public int CategoryId { get; set; } // Зовнішній ключ
        public List<ClothingVariant> Variants { get; set; } // Список розмірів і кількостей одягу
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
    public class ClothingVariant
    {
        [Key]
        public int Id { get; set; } // Первинний ключ
        public ClothingSize Size { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; } // Зовнішній ключ
        public Product Product { get; set; }
    }
    public enum ClothingSize
    {
        S,
        M,
        L,
        XL,
        XXL,
    }
}
