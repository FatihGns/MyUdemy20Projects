using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project9_MongoDbOrde.Services
{
    public class MongoDbConnection
    {
        private IMongoDatabase _database;

        public MongoDbConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017/");//bağlantı adresi
            _database = client.GetDatabase("Db9ProjectOrder");//veritabanı ismi
        }
        public IMongoCollection<BsonDocument> GetOrdersCollection()//tablo isimleri yazacam
        {
            return _database.GetCollection<BsonDocument>("Orders");//tablo ismi
        }
    }
}
