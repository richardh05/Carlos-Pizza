using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carlos_Pizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Carlos_Pizza.Models;

namespace Carlos_Pizza.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly CarlosDB _context;

        public IndexModel(CarlosDB context)
        {
            _context = context;
        }

        public IList<MenuItem> MenuItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MenuItem = await _context.MenuItems.ToListAsync();
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
                MenuItem = _context.MenuItems
                    .Where(m => m.Name.Contains(SearchString))
                    .ToList();
            }
            else
            {
                // If no search string, return all items
                MenuItem = _context.MenuItems.ToList();
            }

            return Page();

        }
        }
}

