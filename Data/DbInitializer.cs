using Carlos_Pizza.Models;

namespace Carlos_Pizza.Data;

public class DbInitializer
{
    public static void Initialize(CarlosDB context) {
        // Look for any food itmes.
        if (context.MenuItems.Any())
        {
            return;   // DB has been seeded
        }

        var MenuItems = new MenuItem[]
        {
            new MenuItem{Name="Shepherds Pie",Desc="Our tasty shepherds pie packed full of lean minced lamb and an assortment of vegetables",Available=true,Vegetarian=false},
            new MenuItem{Name="Cottage Pie",Desc="Our tasty cottage pie packed full of lean minced beef and an assortment of vegetables",Available=true,Vegetarian=false},
            new MenuItem{Name="Haggis,Neeps and Tatties",Desc="Scotland national Haggis dish. Sheep’s heart, liver, and lungs are minced, mixed with suet and oatmeal, then seasoned with onion, cayenne, and our secret spice. Served with boiled turnips and potatoes (‘neeps and tatties’)",Available=true,Vegetarian=false},
            new MenuItem{Name="Bangers and Mash",Desc="Succulent sausages nestled on a bed of buttery mashed potatoes and drenched in a rich onion gravy",Available=true,Vegetarian=false},
            new MenuItem{Name="Toad in the Hole",Desc="Ultimate toad-in-the-hole with caramelised onion gravy",Available=true,Vegetarian=false}
        };

        context.MenuItems.AddRange(MenuItems);
        context.SaveChanges();
    }
}