using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carlos_Pizza.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carlos_Pizza.Models;
using Microsoft.AspNetCore.Authorization;

namespace Carlos_Pizza.Pages.Menu
{
    [Authorize (Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly CarlosDB _context;

        public CreateModel(CarlosDB context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MenuItem MenuItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // check if a file was uploaded
            if (Request.Form.Files.Count > 0)
            {
                var uploadedFile = Request.Form.Files[0]; // only one file
        
                // Check that the file is not null or empty
                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    // Convert the uploaded image file to a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        await uploadedFile.CopyToAsync(memoryStream);
                        MenuItem.Image = memoryStream.ToArray(); // Store byte array in ImageData
                    }
                }
            }
            
            _context.MenuItems.Add(MenuItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
