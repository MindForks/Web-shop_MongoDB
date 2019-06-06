using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web_shop_MongoDB.Entities
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int CountInStock { get; set; }

        [BsonIgnoreIfNull]
        public Manufacturer Manufacturer { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} \nTitle: {1} \nPrice: {2} \nCountInStock: {3} \nManufacturer.Title: {4} \nManufacturer.Description: {5} \n--------", Id, Title, Price, CountInStock, Manufacturer.Title, Manufacturer.Description);
        }
    }
}
