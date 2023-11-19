using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSite.Interfaces;

namespace WebSite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRealization _user;
        public UserController(IUserRealization user)
        {
            _user = user;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _user.UserOrders();
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersOrders()
        {
            var orders = await _user.GetAllUsersOrders();
            return View(orders);
        }
    }
}
