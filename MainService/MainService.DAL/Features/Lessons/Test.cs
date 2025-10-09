using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MainService.DAL.Features.Lessons;

public class Test
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Title { get; set; } = null!;
    public List<Question> Questions { get; set; } = new();
}