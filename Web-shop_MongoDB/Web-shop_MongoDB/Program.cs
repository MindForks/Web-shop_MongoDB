using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;

using Web_shop_MongoDB.Services;

namespace Web_shop_MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            OrderService orderService = new OrderService();
            ProductService productService = new ProductService();
            Console.WriteLine("-----------------");
            Console.WriteLine("Main menu");
            Console.WriteLine("1. Print all products");
            Console.WriteLine("2. Get product by ID");
            Console.WriteLine("3. Create product");
            Console.WriteLine("4. Delete product");
            Console.WriteLine("5. Update product");
            Console.WriteLine("6. Print all orders");
            Console.WriteLine("7. Get order by ID");
            Console.WriteLine("8. Create order");
            Console.WriteLine("9. Delete order");
            Console.WriteLine("10. Update order");
            Console.WriteLine("11. Exit");
            Console.Write("Select action number: ");
            int selected;
            if(int.TryParse(Console.ReadLine(), out selected))
            {
                switch(selected)
                {
                    case 1: productService.PrintItems();  break;
                    case 2:
                    {
                        Console.Write("Input product ID:");
                        try
                        {
                            var objectID = ObjectId.Parse(Console.ReadLine());
                            Console.WriteLine(productService.Get(objectID));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    case 3: productService.Create(); break;
                    case 4:
                    {
                        Console.Write("Input product ID to delete:");
                        try
                        {
                            var objectID = ObjectId.Parse(Console.ReadLine());
                            productService.Delete(objectID);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    case 5:
                    {
                        Console.Write("Input product ID to update:");
                        try
                        {
                            var objectID = ObjectId.Parse(Console.ReadLine());
                            productService.Update(objectID);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    case 6: orderService.PrintItems(); break;
                    case 7:
                    {
                        Console.Write("Input order ID:");
                        try
                        {
                            var objectID = ObjectId.Parse(Console.ReadLine());
                            Console.WriteLine(orderService.Get(objectID));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    case 8: orderService.Create(); break;
                    case 9:
                    {
                        Console.Write("Input order ID to delete:");
                        try
                        {
                            var objectID = ObjectId.Parse(Console.ReadLine());
                            orderService.Delete(objectID);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    case 10:
                    {
                        Console.Write("Input order ID to update:");
                        try
                        {
                            var objectID = ObjectId.Parse(Console.ReadLine());
                            orderService.Update(objectID);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                    case 11: Environment.Exit(0); break;
                    default: Console.WriteLine("Enter digit between 1 and 9"); break;
                }
            }
            else
            {
                Console.WriteLine("Please, enter int");
            }
            MainMenu();
        }

    }
}
    