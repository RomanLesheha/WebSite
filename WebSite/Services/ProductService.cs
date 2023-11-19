using Microsoft.EntityFrameworkCore;
using WebSite.Data;
using WebSite.Interfaces;
using WebSite.Models;

namespace WebSite.Services
{
    public class ProductService : IProductRealization
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> ProductList(bool showDiscountedOnly)
        {
            IEnumerable<Product> products;

            if (showDiscountedOnly)
            {
                products = _context.Products
                    .Where(p => p.DiscountedPrice != null)
                    .ToList();
            }
            else
            {
                products = _context.Products.ToList();
            }

            return products;
        }
    }
}
