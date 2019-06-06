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
    public class OrderService
    {
        private IDatabase<Order> _mongoItem;
        private ProductService _productService;

        public OrderService()
        {
            _mongoItem = new MongoDatabase<Order>("order");
            _productService = new ProductService();
        }

        public Order Get(ObjectId id)
        {
            return _mongoItem.GetOne(i => i.Id == id);
        }

        public void Create()
        {
            var order = new Order();

            Console.Write("Input order UserName: ");
            order.UserName = Console.ReadLine();

            Console.Write("Input product IDs (comma separated): ");
            var ids = (Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries));
            foreach (var id in ids)
            {
                try
                {
                    var objectID = ObjectId.Parse(id);
                    var product = _productService.Get(objectID);
                    if (product == null)
                        Console.WriteLine("Cannot find product with Id:" + id);
                    else
                        order.ProductIDs.Add(new MongoDBRef("product", id));
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            _mongoItem.InsertOne(order);
            Console.WriteLine("Created");
        }

        public void Delete(ObjectId id)
        {
            var product = _mongoItem.GetOne(i => i.Id == id);
            if (product == null)
                Console.WriteLine("Cannot find order to delete");
            else
            {
                _mongoItem.DeleteOne(i => i.Id == id);
                Console.WriteLine("Deleted");
            }
        }

        public void Update(ObjectId id)
        {
            var order = _mongoItem.GetOne(i => i.Id == id);
            if (order == null)
                Console.WriteLine("Cannot find order");
            else
            {
                var update = Builders<Order>.Update;
                var updates = new List<UpdateDefinition<Order>>();

                Console.Write("Do u want to update UserName ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.Write("Input new UserName: ");
                    updates.Add(update.Set(o => o.UserName, Console.ReadLine()));
                }
                Console.Write("Do u want to update Products ? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    order.ProductIDs.Clear();
                    Console.Write("Input product IDs (comma separated): ");
                    var ids = (Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    foreach (var idF in ids)
                    {
                        try
                        {
                            var objectID = ObjectId.Parse(idF);
                            var product = _productService.Get(objectID);
                            if (product == null)
                                Console.WriteLine("Cannot find product with Id:" + idF);
                            else
                                order.ProductIDs.Add(new MongoDBRef("product", idF));
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    updates.Add(update.Set(o => o.ProductIDs, order.ProductIDs));
                }
                _mongoItem.UpdateOne(o => o.Id == id, update.Combine(updates));
                Console.WriteLine("Updated");
            }
        }

        public void PrintItems()
        {
            Console.WriteLine("All items of collection: order \n--------------------");
            var orders = _mongoItem.Query;
            foreach (var item in orders)
            {
                Console.WriteLine(item.ToString());
            }
        }

    }
}
