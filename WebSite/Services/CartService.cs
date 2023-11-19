using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebSite.Data;
using WebSite.Interfaces;
using WebSite.Models;

namespace WebSite.Services
{
    public class CartService : ICartRepository
    {
        private readonly IUserRealization  _user;
        private readonly ApplicationDbContext _db;
        public CartService(ApplicationDbContext db, IUserRealization user)
        {
            _user = user;
            _db = db;
        }
        public async Task<int> AddItem(int ProductId,string ProductSize ,int qty)
        {
            var userId = _user.GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();

                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == ProductId && a.ProductSize == ProductSize);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var product = _db.Products.Find(ProductId);
                    cartItem = new CartDetail
                    {
                        ProductSize = ProductSize,
                        ProductId = ProductId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = product.DiscountedPrice.HasValue ? product.DiscountedPrice.Value : product.Price
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<bool> DoCheckout(Order info)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                // logic
                // move data from cartDetail to order and order detail then we will remove cart detail
                var userId = _user.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");
                var cart = await GetCart(userId);

                if (cart is null)
                    throw new Exception("Invalid cart");
                var cartDetail = _db.CartDetails
                                    .Where(a => a.ShoppingCartId == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");

                var orderStatus = new OrderStatus
                {
                    StatusId = (int)Status.Pending,
                    StatusName = Status.Pending.ToString(),
                    StatusDescription = "Order has been placed but not processed yet",
                };
                var adress = new CustomerAdressDetail
                {
                    DeliverySity = info.adressDetail.DeliverySity,
                    PostDepartment = info.adressDetail.PostDepartment,
                    FullName= info.adressDetail.FullName,
                    PhoneNumber= info.adressDetail.PhoneNumber,
                };
                var order = new Order
                {
                    CustomerId = userId,
                    OrderDate = DateTime.UtcNow,
                    Status = orderStatus,
                    adressDetail = adress,
                };
                _db.Orders.Add(order);
                _db.SaveChanges();

                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    _db.OrderDetails.Add(orderDetail);
                }
                _db.SaveChanges();

                // removing the cartdetails
                _db.CartDetails.RemoveRange(cartDetail);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = _user.GetUserId();
            }

            var data = _db.ShoppingCarts
             .Include(x => x.CartDetails) // Load related CartDetails
             .Where(x => x.CartDetails.Any()) // Filter to include only ShoppingCarts with associated CartDetails
             .SelectMany(x=>x.CartDetails.Select(x=>x.Id))
             .ToListAsync();

             return data.Result.Count;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = _user.GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Product)
                                  .ThenInclude(a => a.Category)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;
        }

        public async Task<int> RemoveItem(int ProductId)
        {
            string userId = _user.GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == ProductId);
                if (cartItem is null)
                    throw new Exception("Not items in cart");
                
                else if (cartItem.Quantity == 1)
                    _db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }
    }
}
