using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carlos_Pizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Carlos_Pizza.Models;
using Microsoft.AspNetCore.Identity;

namespace Carlos_Pizza.Pages.Menu
{
    public class IndexModel(CarlosDB context, UserManager<IdentityUser> userManager) : PageModel
    {
        public IList<MenuItem> MenuItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MenuItem = await context.MenuItems.ToListAsync();
        }
        // === Searchbar ===
        [BindProperty]
        public string SearchString { get; set; }
        public IActionResult OnPostSearch()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Use LINQ to filter menu items based on the search string
                // Using SQL queries can be insecure, so I'm using the objects here
                MenuItem = context.MenuItems
                    .Where(m => m.Name.Contains(SearchString))
                    .ToList();
            }
            else
            {
                // If no search string, return all items
                MenuItem = context.MenuItems.ToList();
            }

            return Page();

        }

        public async Task<IActionResult> OnPostBuyAsync(int itemId)
        {
            var user = await userManager.GetUserAsync(User);
            CheckoutCustomer customer = await context.CheckoutCustomers.FindAsync(user.Email);
            
            var item = context.BasketItems.FromSqlRaw("SELECT * FROM BasketItems WHERE StockID = {0}" + " AND BasketID = {1}", itemId, customer.BasketId).ToList().FirstOrDefault();

            if (item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    BasketId = customer.BasketId,
                    StockId = itemId,
                    Quantity = 1
                };
                context.BasketItems.Add(newItem);
                await context.SaveChangesAsync();
            }
            else
            {
                item.Quantity++;
                context.Attach(item).State = EntityState.Modified;
                try
                {
                    await context.SaveChangesAsync();
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

