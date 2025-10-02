using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MainService.DAL.Features.Courses.Models;

public class Test
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    [BsonElement("Questions")]
    public List<Question> Questions { get; set; } = new();
}