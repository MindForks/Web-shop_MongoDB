using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Web_shop_MongoDB.Database;
using Web_shop_MongoDB.Entities;
using Web_shop_MongoDB.Interfaces;
using System.Linq;

namespace Web_shop_MongoDB.Services
{
    public class ProductService
    {
        private IDatabase<Product> _mongoItem;

        public ProductService()
        {
            _mongoItem = new MongoDatabase<Product>("product");
        } 

        public Product Get(ObjectId id)
        {
            return _mongoItem.GetOne(i => i.Id == id);
        }

        public void Create()
        {
            var product = new Product();
            Console.Write("Input product Title: ");
            product.Title = Console.ReadLine();

            Console.Write("Input product Price: ");
            double tempDouble;
            if (double.TryParse(Console.ReadLine(), out tempDouble))
                product.Price = tempDouble;

            Console.Write("Input product CountInStock: ");
            int tempInt;
            if (int.TryParse(Console.ReadLine(), out tempInt))
                product.CountInStock = tempInt;

            product.Manufacturer = new Manufacturer();
            Console.Write("Input product manufacturer Title: ");
            product.Manufacturer.Title = Console.ReadLine();

            Console.Write("Input product manufacturer Description: ");
            product.Manufacturer.Description = Console.ReadLine();

            _mongoItem.InsertOne(product);
            Console.WriteLine("Created");
        }

        public void Update(ObjectId id)
        {
            var product = _mongoItem.GetOne(i => i.Id == id);
            if (product == null)
                Console.WriteLine("Cannot find product");
            else
            {
                var update = Builders<Product>.Update;
                var updates = new List<UpdateDefinition<Product>>();

                Console.Write("Do u want to update Title ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.Write("Input new Title: ");
                    updates.Add(update.Set(o => o.Title, Console.ReadLine()));
                }
                Console.Write("Do u want to update Price ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.Write("Input new Price: ");
                    double tempDouble;
                    if (double.TryParse(Console.ReadLine(), out tempDouble))
                        updates.Add(update.Set(o => o.Price, tempDouble));
                }
                Console.Write("Do u want to update CountInStock ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.Write("Input new CountInStock: ");
                    int tempInt;
                    if (int.TryParse(Console.ReadLine(), out tempInt))
                        updates.Add(update.Set(o => o.CountInStock, tempInt));
                }
                Console.Write("Do u want to update manufacturer Title ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.Write("Input new manufacturer Title: ");
                    updates.Add(update.Set(o => o.Manufacturer.Title, Console.ReadLine()));
                }
                Console.Write("Do u want to update manufacturer Description ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.Write("Input new manufacturer Description: ");
                    updates.Add(update.Set(o => o.Manufacturer.Description, Console.ReadLine()));
                }

                _mongoItem.UpdateOne(o => o.Id == id, update.Combine(updates));
                Console.WriteLine("Updated");
            }
        }

        public void Delete(ObjectId id)
        {
            var product = _mongoItem.GetOne(i => i.Id == id);
            if (product == null)
                Console.WriteLine("Cannot find product to delete");
            else
            {
                _mongoItem.DeleteOne(i => i.Id == id);
                Console.WriteLine("Deleted");
            }
        }
      
        public void PrintItems()
        {
            Console.WriteLine("All items of collection: product \n--------------------");
            var products = _mongoItem.Query;
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void InitSomeDefaultProduct()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product()
                {
                    Title = "Phone",
                    Price = 200.45,
                    CountInStock = 1,
                    Manufacturer = new Manufacturer() { Title = "Samsung", Description = "good company" }
                },
                new Product()
                {
                    Title = "Laptop",
                    Price = 1000,
                    CountInStock = 3,
                    Manufacturer = new Manufacturer() { Title = "Apple", Description = "great description" }
                },
                new Product()
                {
                    Title = "Air-condition",
                    Price = 12.32,
                    CountInStock = 2,
                    Manufacturer = new Manufacturer() { Title = "Zimbo", Description = "new company desc" }
                },
                new Product()
                {
                    Title = "Monitor",
                    Price = 123,
                    CountInStock = 5,
                    Manufacturer = new Manufacturer() { Title = "Pholips", Description = "new tecxnologies" }
                },
            };

            _mongoItem.InsertMany(products);
        }

    }
}
