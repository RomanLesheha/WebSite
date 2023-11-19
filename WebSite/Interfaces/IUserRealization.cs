using Microsoft.AspNetCore.Identity;
using WebSite.Models;

namespace WebSite.Interfaces
{
    public interface IUserRealization
    {
        Task<bool> IsLikedProductAsync(int ProductID);

        Task<IEnumerable<Order>> UserOrders();

        Task<IEnumerable<Order>> GetAllUsersOrders();

        Task<IEnumerable<Product>> GetLastVisitedUsersProducts();
        string GetUserId();
       
    }
}
