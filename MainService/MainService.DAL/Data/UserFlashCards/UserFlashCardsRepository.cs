using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Context.MongoDb;
using MongoDB.Driver;

namespace MainService.DAL.Data.UserFlashCards
{
    public class UserFlashCardsRepository : MongoRepository<Features.UserFlashCard.UserFlashCards, string>, IUserFlashCardsRepository
    {
        private readonly IMongoCollection<Features.UserFlashCard.UserFlashCards> _flashcards;
        
        public UserFlashCardsRepository(MongoDbContext context) : base(context, MongoDbCollections.FlashCardsCollectionName)
        {
            _flashcards = context.Flashcards;
        }

        public async Task<List<Features.UserFlashCard.UserFlashCards>> GetCardsForNotificationAsync(
            TimeSpan lifetime,
            TimeSpan interval,
            DateTime now,
            CancellationToken ct)
        {
            var fb = Builders<Features.UserFlashCard.UserFlashCards>.Filter;

            var minCreatedAt = now - lifetime;

            var filter = fb.And(
                fb.Gte(x => x.CreatedAt, minCreatedAt),
                fb.Lte(x => x.CreatedAt, now),
                fb.Or(
                    fb.Eq(x => x.LastNotificationAt, null),
                    fb.Lte(x => x.LastNotificationAt, now - interval)
                )
            );

            return await _flashcards.Find(filter).ToListAsync(ct);
        }
        
        public async Task<bool> TryUpdateLastNotificationTimeAsync(
            string id,
            DateTime? previousValue,
            DateTime newValue,
            CancellationToken ct)
        {
            var fb = Builders<Features.UserFlashCard.UserFlashCards>.Filter;

            var filter = fb.And(
                fb.Eq(x => x.Id, id),
                fb.Eq(x => x.LastNotificationAt, previousValue)
            );

            var update = Builders<Features.UserFlashCard.UserFlashCards>.Update
                .Set(x => x.LastNotificationAt, newValue);

            var result = await _flashcards.UpdateOneAsync(
                filter,
                update,
                cancellationToken: ct);

            return result.ModifiedCount == 1;
        }
    }
}

