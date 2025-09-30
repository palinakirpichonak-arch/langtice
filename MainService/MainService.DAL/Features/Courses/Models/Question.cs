using MongoDB.Bson.Serialization.Attributes;

namespace MainService.DAL.Features.Courses.Models;

public class Question
{
    [BsonElement("QuestionNumber")]
    public int QuestionNumber { get; set; }

    [BsonElement("Sentence")]
    public string Sentence { get; set; } = null!;

    [BsonElement("AnswerOptions")]
    public string[] AnswerOptions { get; set; } = null!;
}