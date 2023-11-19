using WebSite.Models;

namespace WebSite.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddItem(int ProductId,string ProductSize, int qty);
        Task<int> RemoveItem(int ProductId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(Order info);
    }
}
