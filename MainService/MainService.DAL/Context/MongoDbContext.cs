using MongoDB.Driver;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Lessons;

namespace MainService.DAL.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
        public IMongoCollection<Test> Tests => _database.GetCollection<Test>("tests");
        public IMongoCollection<UserFlashCards> Flashcards => _database.GetCollection<UserFlashCards>("flashcards");
        
    }
}