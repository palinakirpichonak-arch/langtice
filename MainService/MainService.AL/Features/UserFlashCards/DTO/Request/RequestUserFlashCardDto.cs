namespace MainService.AL.Features.UserFlashCards.DTO.Request;

public class RequestUserFlashCardDto
{
    public Guid UserId {get; set;} 
    public string? Title  {get; set;} 
    public int Count {get; set;}
}