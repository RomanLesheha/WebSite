using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebSite.Areas.Identity.Data;
using WebSite.Data;
using WebSite.Interfaces;
using WebSite.Models;

namespace WebSite.Services
{
    public class UserServices : IUserRealization
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserServices(IHttpContextAccessor http, UserManager<ApplicationUser> user, ApplicationDbContext context)
        {
            _httpContextAccessor = http;
            _userManager = user;
            _context = context;
        }

        public async Task<bool> IsLikedProductAsync(int ProductID)
        {
            var user = GetUserId();
            if (user == null) { return false; }
            var product = await _context.FavouriteProducts
               .FirstOrDefaultAsync(m => m.ProductID == ProductID.ToString() && m.UserId == user);
            if (product == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Product>> GetLastVisitedUsersProducts()
        {
            var user = GetUserId();
            if (user == null) { return new List<Product>(); }
            var last_visited = _context.LastVisitedsProducts.Where(a => a.UserId == user).Select(p=>p.ProductID).ToList();

            var products = await _context.Products.Where(p => last_visited.Contains(p.Id.ToString())).Take(3).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await _context.Orders
                            .Include(x=>x.Status)
                            .Include(x => x.OrderDetails)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Category)
                            .Where(a => a.CustomerId == userId)
                            .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetAllUsersOrders()
        {
            var orders = await _context.Orders
                           .Include(x => x.Status)
                           .Include(x => x.OrderDetails)
                           .ThenInclude(x => x.Product)
                           .ThenInclude(x => x.Category)
                           .ToListAsync();
            return orders;
        }


        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
