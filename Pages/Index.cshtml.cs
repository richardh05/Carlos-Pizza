using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Carlos_Pizza.Data;
using Carlos_Pizza.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlos_Pizza.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, CarlosDB context)
        {
            _logger = logger;
            _db = context;
        }
        
        [BindProperty]
        public string Search { get; set; }

        public IList<MenuItem> MenuItems { get; set; } = default;
        
        private readonly CarlosDB _db;
        public void OnGet()
        {
            MenuItems = _db.MenuItems.FromSqlRaw("SELECT * FROM MenuItem WHERE Category = 'Main'").ToList();
        }

        public IActionResult OnPostSearch()
        {
            MenuItems = _db.MenuItems
                .FromSqlRaw("SELECT * FROM MenuItem WHERE Name LIKE '" + Search + "%'").ToList();
            return Page();
        }
    }
}
