using MongoDB.Bson.Serialization.Attributes;

namespace MainService.DAL.Features.Test;

public class Question
{
    [BsonId]
    public int QuestionNumber { get; set; }

    [BsonElement("Sentence")]
    public string Sentence { get; set; } = null!;

    [BsonElement("AnswerOptions")]
    public string[] AnswerOptions { get; set; } = null!;
    
    [BsonElement("CorrectAnswer")]
    public string CorrectAnswer { get; set; } = null!;
}