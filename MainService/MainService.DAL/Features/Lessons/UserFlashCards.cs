using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MainService.DAL.Features.Lessons;

public class UserFlashCards
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public List<FlashCard> Items { get; set; } = new();
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; }
}