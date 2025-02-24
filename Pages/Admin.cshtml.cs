using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Carlos_Pizza.Pages;

[Authorize (Roles = "Admin")]
public class Admin : PageModel
{
    public void OnGet()
    {
        
    }
}