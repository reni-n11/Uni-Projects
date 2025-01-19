using System;
using System.Globalization;
using System.Text;

namespace Restaurant
{
    //Ресторант
    //Добавяне на продукт: [категория(салата, супа, осgit новно ястие, десерт, напитка)], [име], [грамаж], [цена]
    //Добавяне на поръчка: [номер на маса], [име на продукт 1], [име на продукт 2], ...
    //Информация за продукт: инфо [име на продукт]
    //Информация за продажбите за деня: продажби
    //Изход: изход
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            List<Salad> saladsList = new();
            List<Soup> soupsList = new();
            List<MainDish> mainDishesList = new();
            List<Dessert> dessertsList = new();
            List<Drink> drinksList = new();
            List<Product> menuList = new();
            Dictionary<int, List<Product>> tablesDict = new();

            string input = Console.ReadLine();
            string[] contentsArr = input.Split(", ");
            string firstElement = contentsArr[0];

            int saladsCount = 0;
            int soupsCount = 0;
            int mainDishesCount = 0;
            int dessertsCount = 0;
            int drinksCount = 0;

            decimal saladsSum = 0;
            decimal soupsSum = 0;
            decimal mainDishesSum = 0;
            decimal dessertsSum = 0;
            decimal drinksSum = 0;
            decimal totalSum = 0;

            while (!input.Equals("изход"))
            {
                bool exit = false;
                while (!int.TryParse(firstElement, out int productType) && contentsArr.Length > 2)
                {
                    string foodCategory = firstElement;
                    string productName = contentsArr[1];
                    int productWeight = int.Parse(contentsArr[2]);
                    decimal productPrice = decimal.Parse(contentsArr[3], provider: CultureInfo.InvariantCulture);

                    switch (foodCategory)
                    {
                        case "салата":
                            Salad salad = new(productName, productWeight, productPrice);
                            saladsList.Add(salad);
                            menuList.Add(salad);
                            break;
                        case "супа":
                            Soup soup = new(productName, productWeight, productPrice);
                            soupsList.Add(soup);
                            menuList.Add(soup);
                            break;
                        case "основно ястие":
                            MainDish mainD = new(productName, productWeight, productPrice);
                            mainDishesList.Add(mainD);
                            menuList.Add(mainD);
                            break;
                        case "десерт":
                            Dessert dessert = new(productName, productWeight, productPrice);
                            dessertsList.Add(dessert);
                            menuList.Add(dessert);
                            break;
                        case "напитка":
                            Drink drink = new(productName, productWeight, productPrice);
                            drinksList.Add(drink);
                            menuList.Add(drink);
                            break;
                    }
                    input = Console.ReadLine();
                    contentsArr = input.Split(", ");
                    firstElement = contentsArr[0];
                }
                

                while (int.TryParse(firstElement, out int tableNumber))
                {
                    //use the classes instead of lists

                    List<Product> ordersPerTableList = new();


                    //go thru all orders for the table
                    for (int product = 1; product < contentsArr.Length; product++)
                    {   //go thru all salads
                        bool typeFound = false;
                        
                        foreach (Salad salad in saladsList)
                        {   //if order is a salad
                            if (contentsArr[product].Equals(salad.Name))
                            {
                                saladsCount++;
                                saladsSum += salad.Price;
                                ordersPerTableList.Add(salad);
                                typeFound = true;
                                break;
                            }
                        }
                        if (typeFound)
                        {
                            continue;
                        }
                        foreach (Soup soup in soupsList)
                        {
                            if (contentsArr[product].Equals(soup.Name))
                            {
                                soupsCount++;
                                soupsSum += soup.Price;
                                ordersPerTableList.Add(soup);
                                typeFound = true;
                                break;
                            }
                        }
                        if (typeFound)
                        {
                            continue;
                        }
                        foreach (MainDish mainDish in mainDishesList)
                        {
                            if (contentsArr[product].Equals(mainDish.Name))
                            {
                                mainDishesCount++;
                                mainDishesSum += mainDish.Price;
                                ordersPerTableList.Add(mainDish);
                                typeFound = true;
                                break;
                            }
                        }
                        if (typeFound)
                        {
                            continue;
                        }
                        foreach (Dessert dessert in dessertsList)
                        {
                            if (contentsArr[product].Equals(dessert.Name))
                            {
                                dessertsCount++;
                                dessertsSum += dessert.Price;
                                ordersPerTableList.Add(dessert);
                                typeFound = true;
                                break;
                            }
                        }
                        if (typeFound)
                        {
                            continue;
                        }
                        foreach (Drink drink in drinksList)
                        {
                            if (contentsArr[product].Equals(drink.Name))
                            {
                                drinksCount++;
                                drinksSum += drink.Price;
                                ordersPerTableList.Add(drink);
                                typeFound = true;
                                break;
                            }
                        }
                        if (typeFound)
                        {
                            continue;
                        }
                    }
                    

                    if (tablesDict.TryGetValue(int.Parse(firstElement), out List<Product> value))
                    {
                        value.AddRange(ordersPerTableList);
                    }
                    else
                    {
                        tablesDict.Add(int.Parse(firstElement), ordersPerTableList);
                    }

                    input = Console.ReadLine();
                    contentsArr = input.Split(", ");
                    firstElement = contentsArr[0];
                }
                
                switch (input.Split(" ")[0])
                {
                    case "продажби":
                        
                        totalSum = 0;
                        foreach (KeyValuePair<int, List<Product>> table in tablesDict)
                        {
                            foreach (Product product in table.Value)
                            {
                                totalSum += product.Price;
                            }
                        }

                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine($"Общо заети маси през деня: {tablesDict.Count}");
                        Console.WriteLine($"Общо продажби: {tablesDict.Values.Sum(list => list.Count)} - {totalSum
                        .ToString("0.00", CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"По категории:");
                        Console.WriteLine($"- Салата: {saladsCount} – {saladsSum
                        .ToString("0.00", CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"- Супа: {soupsCount} – {soupsSum
                        .ToString("0.00", CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"- Основно ястие: {mainDishesCount} – {mainDishesSum
                        .ToString("0.00", CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"- Десерт: {dessertsCount} – {dessertsSum
                        .ToString("0.00", CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"- Напитка: {drinksCount} – {drinksSum
                        .ToString("0.00", CultureInfo.InvariantCulture)}");
                        break;
                    case "инфо":

                        string searchProduct = "";

                        if (input.Split(" ").Length > 2)
                        {
                            searchProduct = string.Join(" ", input.Split(" ").Skip(1).ToArray());
                        }
                        else
                        {
                            searchProduct = input.Split(" ")[1];
                        }

                        foreach (Product product in menuList)
                        {
                            if (product.Name.Equals(searchProduct))
                            {
                                Console.WriteLine($"Информация за продукт: {product.Name}");

                                if (product is Salad)
                                {
                                    Salad s = (Salad)product;
                                    Console.WriteLine($"Грамаж: {s.Grams}");
                                    
                                }
                                else if (product is Soup)
                                {
                                    Soup so = (Soup)product;
                                    Console.WriteLine($"Грамаж: {so.Grams}");
                                }
                                else if (product is MainDish)
                                {
                                    MainDish md = (MainDish)product;
                                    Console.WriteLine($"Грамаж: {md.Grams}");
                                    Console.WriteLine($"Калории: {md.Calories}");
                                }
                                else if (product is Dessert)
                                {
                                    Dessert de = (Dessert)product;
                                    Console.WriteLine($"Грамаж: {de.Grams}");
                                    Console.WriteLine($"Калории: {de.Calories}");
                                }
                                else if (product is Drink)
                                {
                                    Drink dr = (Drink)product;
                                    Console.WriteLine($"Грамаж: {dr.Mililiters}");
                                    Console.WriteLine($"Калории: {dr.Calories}");
                                }
                                break;
                            }
                        }
                            
                        break;
                }
                if (input.Equals("изход"))
                {
                    break;
                }
                input = Console.ReadLine();
                contentsArr = input.Split(", ");
                firstElement = contentsArr[0];
            }

            totalSum = 0;

            foreach (KeyValuePair<int, List<Product>> table in tablesDict)
            {
                foreach (Product product in table.Value)
                {
                    totalSum += product.Price; ;
                }
            }

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Общо заети маси през деня: {tablesDict.Count}");
            Console.WriteLine($"Общо продажби: {tablesDict.Values.Sum(list => list.Count)} - {totalSum
            .ToString("0.00", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"По категории:");
            Console.WriteLine($"- Салата: {saladsCount} – {saladsSum
            .ToString("0.00", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"- Супа: {soupsCount} – {soupsSum
            .ToString("0.00", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"- Основно ястие: {mainDishesCount} – {mainDishesSum
            .ToString("0.00", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"- Десерт: {dessertsCount} – {dessertsSum
            .ToString("0.00", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"- Напитка: {drinksCount} – {drinksSum
            .ToString("0.00", CultureInfo.InvariantCulture)}");
        }
    }
}