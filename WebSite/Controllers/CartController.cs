using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSite.Interfaces;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int ProductID, string size, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(ProductID, size, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int ProductID)
        {
            var cartCount = await _cartRepo.RemoveItem(ProductID);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public  async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order model)
        {
                bool isCheckedOut = await _cartRepo.DoCheckout(model);
                if (!isCheckedOut)
                    throw new Exception("Something happen in server side");
            return RedirectToAction("Index", "Home");
        }
    }
}
