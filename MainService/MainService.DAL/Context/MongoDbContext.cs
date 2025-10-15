using MainService.DAL.Configurations.MongoDb;
using MongoDB.Driver;
using MainService.DAL.Features.Lessons;

namespace MainService.DAL.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Test> Tests { get; private set; }
        public IMongoCollection<UserFlashCards> Flashcards { get; private set; }

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            
            Tests = new TestConfiguration().Initialize(_database);
            Flashcards = new UserFlashCardsConfiguration().Initialize(_database);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) 
            => _database.GetCollection<T>(collectionName);
    }
}