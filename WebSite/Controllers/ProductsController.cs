using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebSite.Areas.Identity.Data;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProductsController(IHttpContextAccessor http, UserManager<ApplicationUser> user, ApplicationDbContext context)
        {
            _httpContextAccessor = http;
            _userManager = user;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var shoppingContext = _context.Products.Include(p => p.Category);
            return View(await shoppingContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
               .Include(p => p.Category).Include(p => p.Variants)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var user = GetUserId();
            if (user != null)
            {
                var LastVisited = new LastVisited()
                {
                    ProductID = id.ToString(),
                    UserId = user
                };
                if (_context.LastVisitedsProducts.Any(e => e.UserId == user && e.ProductID == id.ToString()))
                    return View(product);
                else
                {
                    var product_add = _context.LastVisitedsProducts.Add(LastVisited);
                    _context.SaveChanges();
                }
            }
            return View(product);
        }


        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Створіть новий продукт та додайте варіанти одягу
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    DiscountedPrice = model.DescountedPrice,
                    Photo = model.Photo,
                    CategoryId = model.CategoryId,
                    Variants = new List<ClothingVariant>()
                };

                foreach (var sizeInfo in model.SizesInfo)
                {
                    product.Variants.Add(new ClothingVariant
                    {
                        Size = Enum.Parse<ClothingSize>(sizeInfo.Size),
                        Quantity = sizeInfo.Quantity
                    });
                }

                // Додайте новий продукт до бази даних і збережіть зміни
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(x => x.Variants)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,DescountedPrice,Amount,Photo,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ShoppingContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<bool> AddToFavourite(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return false;
            }

            var user = GetUserId();
            var product = await _context.Products.FindAsync(id);
            if (product != null && user != null)
            {
                _context.FavouriteProducts.Add(new FavouriteProducts
                {
                    UserId = user,
                    ProductID = product.Id.ToString()
                });

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<bool> RemoveFromFavourite(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return false;
            }
            var user = GetUserId();
            var product = await _context.FavouriteProducts
                .FirstOrDefaultAsync(m => m.ProductID == id.ToString() && m.UserId == user);
            if (product != null && user != null)
            {
                _context.FavouriteProducts.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
