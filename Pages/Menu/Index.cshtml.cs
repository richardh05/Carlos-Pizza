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
    }
}
