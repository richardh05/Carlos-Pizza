using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Carlos_Pizza.Data;
using Carlos_Pizza.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Carlos_Pizza.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, CarlosDB context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _db = context;
            _userManager = userManager;
        }
        

        public IList<MenuItem> MenuItems { get; set; } = default;
        
        private readonly CarlosDB _db;
        public void OnGet()
        {
            MenuItems = _db.MenuItems.FromSqlRaw("SELECT * FROM MenuItems WHERE Category = 'Main'").ToList();
        }
        public async Task<IActionResult> OnPostBuyAsync(int itemId)
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _db.CheckoutCustomers.FindAsync(user.Email);
            
            var item = _db.BasketItems.FromSqlRaw("SELECT * FROM BasketItems WHERE StockID = {0}" + " AND BasketID = {1}", itemId, customer.BasketId).ToList().FirstOrDefault();

            if (item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    BasketId = customer.BasketId,
                    StockId = itemId,
                    Quantity = 1
                };
                _db.BasketItems.Add(newItem);
                await _db.SaveChangesAsync();
            }
            else
            {
                item.Quantity++;
                _db.Attach(item).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Basket not found!", e);
                }
            }
            return RedirectToPage();
        }
    }
    }