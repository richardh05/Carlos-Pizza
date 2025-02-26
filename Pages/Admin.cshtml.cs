using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Carlos_Pizza.Data;
using Carlos_Pizza.Models;

namespace Carlos_Pizza.Pages;

[Authorize (Roles = "Admin")]
public class Admin : PageModel
{
    private readonly ILogger<Admin> _logger;

    public Admin(ILogger<Admin> logger, CarlosDB context)
    {
        _logger = logger;
        _db = context;
    }
    
    public IList<MenuItem> MenuItems { get; set; } = default;
        
    private readonly CarlosDB _db;
    public void OnGet()
    {
        MenuItems = _db.MenuItems.FromSqlRaw("SELECT * FROM MenuItem").ToList();
    }
}