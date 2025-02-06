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
                // == Mains ==
                new MenuItem{Name="Margherita Pizza", Desc="A classic pizza topped with fresh mozzarella, tomato sauce, basil, and a drizzle of olive oil.", Available=true, Vegetarian=true, Category="Main"},
                new MenuItem{Name="Pepperoni Pizza", Desc="Loaded with savory pepperoni slices, mozzarella cheese, and marinara sauce.", Available=true, Vegetarian=false, Category="Main"},
                new MenuItem{Name="Veggie Pizza", Desc="A delightful mix of fresh vegetables including bell peppers, onions, mushrooms, olives, and tomatoes on a mozzarella base.", Available=true, Vegetarian=true, Category="Main"},
                new MenuItem{Name="BBQ Chicken Pizza", Desc="Grilled chicken, red onions, barbecue sauce, and mozzarella cheese, perfect for BBQ lovers.", Available=true, Vegetarian=false, Category="Main"},
                new MenuItem{Name="Meat Lover's Pizza", Desc="A hearty pizza with pepperoni, sausage, bacon, and ham, all topped with melted mozzarella cheese.", Available=true, Vegetarian=false, Category="Main"},
                new MenuItem{Name="Hawaiian Pizza", Desc="A sweet and savory combination of ham, pineapple, and mozzarella cheese.", Available=true, Vegetarian=false, Category="Main"},
                new MenuItem{Name="Four Cheese Pizza", Desc="A rich pizza topped with a blend of mozzarella, cheddar, parmesan, and goat cheese.", Available=true, Vegetarian=true, Category="Main"},

                // == Sides ==
                new MenuItem{Name="Garlic Bread", Desc="Warm, buttery garlic bread topped with a sprinkle of parmesan and parsley.", Available=true, Vegetarian=true, Category="Sides"},
                new MenuItem{Name="Caesar Salad", Desc="Crisp romaine lettuce tossed with Caesar dressing, parmesan cheese, and croutons.", Available=true, Vegetarian=true, Category="Sides"},
                new MenuItem{Name="Mozzarella Sticks", Desc="Breaded and fried to golden perfection, served with marinara dipping sauce.", Available=true, Vegetarian=true, Category="Sides"},
                new MenuItem{Name="French Fries", Desc="Classic french fries. Hot, fresh, and crispy, served with ketchup.", Available=true, Vegetarian=true, Category="Sides"},

                // == Drinks ==
                new MenuItem{Name="Coke", Available=true, Vegetarian=true, Category="Drinks"},
                new MenuItem{Name="Diet Coke", Available=true, Vegetarian=true, Category="Drinks"},
                new MenuItem{Name="Sprite", Available=true, Vegetarian=true, Category="Drinks"},
                new MenuItem{Name="Fanta", Available=true, Vegetarian=true, Category="Drinks"},
            };

            context.MenuItems.AddRange(MenuItems);
            context.SaveChanges();
        }
    }
}
