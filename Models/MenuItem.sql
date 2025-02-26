-- DbInitializer stopped working for me, so I'm using this for now
DROP TABLE IF EXISTS MenuItem;

CREATE TABLE MenuItem (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(40) NOT NULL,
    [Desc] NVARCHAR(255),             
    Category NVARCHAR(40),
    Available BIT,                    
    Vegetarian BIT,               
    Price MONEY NOT NULL,
    Image VARBINARY(MAX),
);


INSERT INTO MenuItem (Name, Category, [Desc], Price, Available, Vegetarian)
VALUES
    -- Mains
    ('Margherita Pizza', 'Main', 'A classic pizza topped with fresh mozzarella, tomato sauce, basil, and a drizzle of olive oil.', 9.90, 1, 1),
    ('Pepperoni Pizza', 'Main', 'Loaded with savory pepperoni slices, mozzarella cheese, and marinara sauce.', 12.70, 1, 0),
    ('Veggie Pizza', 'Main', 'A delightful mix of fresh vegetables including bell peppers, onions, mushrooms, olives, and tomatoes on a mozzarella base.', 15.50, 1, 1),
    ('BBQ Chicken Pizza', 'Main', 'Grilled chicken, red onions, barbecue sauce, and mozzarella cheese, perfect for BBQ lovers.', 14.10, 1, 0),
    ('Meat Lover''s Pizza', 'Main', 'A hearty pizza with pepperoni, sausage, bacon, and ham, all topped with melted mozzarella cheese.', 14.10, 1, 0),
    ('Hawaiian Pizza', 'Main', 'A sweet and savory combination of ham, pineapple, and mozzarella cheese.', 12.70, 1, 0),
    ('Four Cheese Pizza', 'Main', 'A rich pizza topped with a blend of mozzarella, cheddar, parmesan, and goat cheese.', 12.70, 1, 1),

    -- Sides
    ('Garlic Bread', 'Sides', 'Warm, buttery garlic bread topped with a sprinkle of parmesan and parsley.', 9.90, 1, 1),
    ('Loaded Salad', 'Sides', 'Delight mozzarella perfectly melted over peppers, tomato and sweetcorn.', 3.00, 1, 1),
    ('Chicken Strips', 'Sides', 'Breaded and fried to golden perfection, served with marinara dipping sauce.', 4.70, 1, 0),
    ('French Fries', 'Sides', 'Classic french fries. Hot, fresh, and crispy, served with ketchup.', 3.00, 1, 1),

    -- Drinks
    ('Coke', 'Drinks', 'A refreshing can of Coke', 1.20, 1, 1),
    ('Diet Coke', 'Drinks', 'A refreshing can of Diet Coke', 1.20, 1, 1),
    ('Sprite', 'Drinks', 'A refreshing can of Sprite', 1.20, 1, 1),
    ('Fanta', 'Drinks', 'A refreshing can of Fanta', 1.20, 1, 1);