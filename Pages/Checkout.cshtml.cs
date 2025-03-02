using Carlos_Pizza.Data;
using Carlos_Pizza.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Carlos_Pizza.Pages;
using Microsoft.AspNetCore.Mvc;

// Create variables to hold the logged the database (_db), the UserManager tool (_UserManager) and to hold all the items (Items).
public class CheckoutModel(CarlosDB db, UserManager<IdentityUser> userManager) : PageModel
{
    public IList<CheckoutItem> Items { get; private set; }
    
    public decimal Total;
    public long AmountPayable;
    public OrderHistory Order = new OrderHistory();

    // Instantiate the values for the database and the user manager through constructor dependency injection

    // OnGet is called when the web page first loads, we use async calls because it will access the database
    public async Task OnGetAsync()
    {
        // Use the UserManager to find out who the logged in user is.  From this we can find out the user’s email address and therefore the record which matches the user in the CheckoutCustomer table.
        var user = await userManager.GetUserAsync(User);
        
            CheckoutCustomer customer = await db.CheckoutCustomers.FindAsync(user.Email);

            // Perform the SQL search to find all the data we need.  Notice how it uses the CheckoutItems class we create earlier.
                Items = db.CheckoutItems.FromSqlRaw(
                    "SELECT MenuItem.ID, MenuItem.Price, " +
                    "MenuItem.Name, " +
                    "BasketItems.BasketId, BasketItems.Quantity " +
                    "FROM MenuItem INNER JOIN BasketItems " +
                    "ON MenuItem.Id = BasketItems.StockID " +
                    "WHERE BasketID = {0}", customer.BasketId
                ).ToList();
        

        Total = 0;
        foreach (var item in Items)
        {
            Total += (item.Quantity * item.Price);
        }
        AmountPayable = (long)Total;
    }
    
    // Process the buy click
    public async Task<IActionResult> OnPostBuyAsync()
    {
        // var currentOrder = db.OrderHistories.FromSqlRaw("SELECT * From OrderHistories")
        //     .OrderByDescending(b => b.OrderNo)
        //     .FirstOrDefault();
        //
        // if (currentOrder == null)
        // {
        //     Order.OrderNo = 1;
        // }
        // else
        // {
        //     Order.OrderNo = currentOrder.OrderNo + 1;
        // }

        var user = await userManager.GetUserAsync(User);
        var customer = await db.CheckoutCustomers.FindAsync(user.Email);
        
        Order.Email = user.Email;
        db.OrderHistories.Add(Order);
        await db.SaveChangesAsync();  // This will auto-generate the OrderNo for the Order

        // CheckoutCustomer customer = await db
        //     .CheckoutCustomers
        //     .FindAsync(user.Email);

        var basketItems =
            db.BasketItems
                .FromSqlRaw("SELECT * From BasketItems WHERE BasketID = {0}", customer.BasketId)
                .ToList();

        foreach (var item in basketItems)
        {
            OrderItems oi = new OrderItems
            {
                OrderId = Order.OrderNo,
                StockId = item.StockId,
                Quantity = item.Quantity
            };
            db.OrderItems.Add(oi);
            db.BasketItems.Remove(item);
        }
        await db.SaveChangesAsync();
        return RedirectToPage("/Index");
    }
}