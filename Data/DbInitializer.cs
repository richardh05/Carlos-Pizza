using Carlos_Pizza.Models;

namespace Carlos_Pizza.Data
{
    public class DbInitializer
    {
        public static void Initialize(CarlosDB context) {
            // Look for any food items.
            if (context.MenuItems.Any())
            {
                return;   // DB has been seeded
            }

            var MenuItems = new MenuItem[]
            {
                // Id's don't need to be declared because ASP automates them
                // == Mains ==
                new MenuItem{Name="Margherita Pizza", Category="Main", Desc="A classic pizza topped with fresh mozzarella, tomato sauce, basil, and a drizzle of olive oil.", Price=9.90, Available=true, Vegetarian=true},
                new MenuItem{Name="Pepperoni Pizza", Category="Main", Desc="Loaded with savory pepperoni slices, mozzarella cheese, and marinara sauce.", Price=12.70,Available=true, Vegetarian=false},
                new MenuItem{Name="Veggie Pizza", Category="Main", Desc="A delightful mix of fresh vegetables including bell peppers, onions, mushrooms, olives, and tomatoes on a mozzarella base.", Price=15.50, Available=true, Vegetarian=true},
                new MenuItem{Name="BBQ Chicken Pizza", Category="Main", Desc="Grilled chicken, red onions, barbecue sauce, and mozzarella cheese, perfect for BBQ lovers.", Price=14.10,Available=true, Vegetarian=false},
                new MenuItem{Name="Meat Lover's Pizza", Category="Main", Desc="A hearty pizza with pepperoni, sausage, bacon, and ham, all topped with melted mozzarella cheese.", Price=14.10, Available=true, Vegetarian=false},
                new MenuItem{Name="Hawaiian Pizza", Category="Main", Desc="A sweet and savory combination of ham, pineapple, and mozzarella cheese.", Price=12.70, Available=true, Vegetarian=false},
                new MenuItem{Name="Four Cheese Pizza", Category="Main", Desc="A rich pizza topped with a blend of mozzarella, cheddar, parmesan, and goat cheese.", Price=12.70, Available=true, Vegetarian=true},

                // == Sides ==
                new MenuItem{Name="Garlic Bread", Category="Sides", Desc="Warm, buttery garlic bread topped with a sprinkle of parmesan and parsley.", Price=9.90, Available=true, Vegetarian=true},
                new MenuItem{Name="Loaded Salad", Category="Sides", Desc="Delight mozzarella perfectly melted over peppers, tomato and sweetcorn.", Price=3.00, Available=true, Vegetarian=true},
                new MenuItem{Name="Chicken Strips", Category="Sides", Desc="Breaded and fried to golden perfection, served with marinara dipping sauce.", Price=4.70, Available=true, Vegetarian=true},
                new MenuItem{Name="French Fries", Category="Sides", Desc="Classic french fries. Hot, fresh, and crispy, served with ketchup.", Price=3.00, Available=true, Vegetarian=true},

                // == Drinks ==
                new MenuItem{Name="Coke", Category="Drinks", Desc="A refreshing can of Coke", Price=1.20, Available=true, Vegetarian=true, },
                new MenuItem{Name="Diet Coke", Category="Drinks", Desc="A refreshing can of Diet Coke", Price=1.20, Available=true, Vegetarian=true},
                new MenuItem{Name="Sprite", Category="Drinks", Desc="A refreshing can of Diet Coke", Price=1.20, Available=true, Vegetarian=true},
                new MenuItem{Name="Fanta", Category="Drinks", Price=1.20, Available=true, Vegetarian=true},
            };

            context.MenuItems.AddRange(MenuItems);
            context.SaveChanges();
        }
    }
}
