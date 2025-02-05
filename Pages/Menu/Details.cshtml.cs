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
    public class DetailsModel : PageModel
    {
        private readonly CarlosDB _context;

        public DetailsModel(CarlosDB context)
        {
            _context = context;
        }

        public MenuItem MenuItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuitem = await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
            if (menuitem == null)
            {
                return NotFound();
            }
            else
            {
                MenuItem = menuitem;
            }
            return Page();
        }
    }
}
