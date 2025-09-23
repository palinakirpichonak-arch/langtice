namespace MainService.AL.Words.DTO;

public class WordDTO
{
    public string Text { get; set; } = null!;
    public Guid? LanguageId { get; set; }
}