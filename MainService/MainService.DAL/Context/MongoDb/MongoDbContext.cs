using MainService.DAL.Configurations.MongoDb;
using MainService.DAL.Models.TestModel;
using MainService.DAL.Models.UserFlashCardModel;
using MongoDB.Driver;

namespace MainService.DAL.Context.MongoDb
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Test> Tests { get; private set; }
        public IMongoCollection<UserFlashCard> Flashcards { get; private set; }

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public async Task InitializeDbContext(CancellationToken cancellationToken)
        {
            Tests = await new TestConfiguration().InitializeAsync(_database, cancellationToken);
            Flashcards = await new UserFlashCardsConfiguration().InitializeAsync(_database, cancellationToken);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName) 
            => _database.GetCollection<T>(collectionName);
    }
}