using WebSite.Models;

namespace WebSite.Interfaces
{
    public interface IProductRealization
    {
        IEnumerable<Product> ProductList(bool showDiscountedOnly);
    }
}
